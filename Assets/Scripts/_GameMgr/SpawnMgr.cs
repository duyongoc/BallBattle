using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMgr : MonoBehaviour
{
    [Header("Param of the game")]
    public ScriptAttacker scriptAttacker;
    public ScriptDefender scriptDefender;

    [Header("All of energy of the game")]
    public float maxEnergy;
    public float enemyEnergy;
    public float playerEnergy;
    public float delayAmount = 1;

    public Slider sliderEnemyEnergy;
    public Slider sliderPlayerEnergy;

    // 0 is enemy - 1 is player
    public Transform[] transTxtEnergy;
    public GameObject txtEnergy;

    [Header("Object")]
    public GameObject prefabAttacker;
    public GameObject prefabDefender;

    [Header("The Ball")]
    public GameObject prefabBall;

    [Header("List object was created")]
    public List<GameObject> listAttacker;
    public List<GameObject> listDefender;

    //
    //private variable
    //
    private GameObject tmp;
    private GameObject ballTmp;

    private float timer = 0;
    private float energyRegenEnemy = 0;
    private float energyRegenPlayer = 0;
    private float energyDefenderCost = 0;
    private float energyAttackerCost = 0;

    #region singleton
    public static SpawnMgr s_instance;

    private void Awake()
    {
        if (s_instance != null)
        {
            return;
        }
        s_instance = this;
    }
    #endregion    

    private void LoadData()
    {
        energyRegenEnemy = scriptDefender.energyRegen;
        energyRegenPlayer = scriptAttacker.energyRegen;

        energyDefenderCost = scriptDefender.energyDefenderCost;
        energyAttackerCost = scriptAttacker.energyAttackerCost;
    }

    #region UNITY
    private void Start()
    {
        LoadData();

    }

    private void FixedUpdate()
    {
        if(!GameMgr.GetInstance().IsStateInGame())
            return;
            
        timer += Time.deltaTime;
        if (timer >= delayAmount)
        {
            enemyEnergy += energyRegenEnemy;
            playerEnergy += energyRegenPlayer;

            // show energy slider on the UI
            sliderEnemyEnergy.value = enemyEnergy / maxEnergy;
            sliderPlayerEnergy.value = playerEnergy / maxEnergy;

            timer = 0;
        }
    }
    #endregion


    #region CREATE OBJECT
    public void CreateTheBall()
    {
        if (LandMgr.GetInstance().IsPhaseDown())
        {
            float randX = Random.Range(-7.5f, 7.5f);
            float randZ = Random.Range(-12f, 0f);

            ballTmp = Instantiate(prefabBall, new Vector3(randX, 0f, randZ), Quaternion.identity);
        }
        else if (LandMgr.GetInstance().IsPhaseUp())
        {
            float randX = Random.Range(-7.5f, 7.5f);
            float randZ = Random.Range(0f, 12f);

            ballTmp = Instantiate(prefabBall, new Vector3(randX, 0f, randZ), Quaternion.identity);
        }
    }

    public void SpawnAttacker(Vector3 position)
    {
        if (LandMgr.GetInstance().IsPhaseDown())
        {
            tmp = Instantiate(prefabAttacker, position, Quaternion.Euler(0f, 0f, 0f));
        }
        if (LandMgr.GetInstance().IsPhaseUp())
        {
            tmp = Instantiate(prefabAttacker, position, Quaternion.Euler(0f, 180f, 0f));
        }

        listAttacker.Add(tmp);
    }

    public void SpawnDefender(Vector3 position)
    {

        if (LandMgr.GetInstance().IsPhaseDown())
        {
            tmp = Instantiate(prefabDefender, position, Quaternion.Euler(0f, 180f, 0f));
        }
        if (LandMgr.GetInstance().IsPhaseUp())
        {
            tmp = Instantiate(prefabDefender, position, Quaternion.Euler(0f, 0f, 0f));
        }

        listDefender.Add(tmp);
    }
    #endregion


    #region Get energy when spawning object 
    public bool UsingEnemyEnergy()
    {
        // Enemy get phase down when spawning the defender
        if (LandMgr.GetInstance().IsPhaseDown())
        {
            if (enemyEnergy - energyDefenderCost < 0)
            {
                CreateTextEnemyEnergy(energyDefenderCost, true);
                return false;
            }
               
            enemyEnergy -= energyDefenderCost;
            sliderEnemyEnergy.value = enemyEnergy / maxEnergy;
            CreateTextEnemyEnergy(energyDefenderCost, false);
        }
        // Enemy get phase down when spawning the attacker
        if (LandMgr.GetInstance().IsPhaseUp())
        {
            if (enemyEnergy - energyAttackerCost < 0)
            {
                CreateTextEnemyEnergy(energyAttackerCost, true);
                return false;
            }
                
            enemyEnergy -= energyAttackerCost;
            sliderEnemyEnergy.value = enemyEnergy / maxEnergy;
            CreateTextEnemyEnergy(energyAttackerCost, false);
        }

        return true;
    }

    public bool UsingPlayerEnergy()
    {
        // player get phase down when spawning the attacker 
        if (LandMgr.GetInstance().IsPhaseDown())
        {
            if (playerEnergy - energyAttackerCost < 0)
            {
                CreateTextPlayerEnergy(energyAttackerCost, true);
                return false;
            }

            playerEnergy -= energyAttackerCost;
            sliderPlayerEnergy.value = playerEnergy / maxEnergy;
            CreateTextPlayerEnergy(energyAttackerCost, false);
        }
        // player get phase up when spawning the defender 
        if (LandMgr.GetInstance().IsPhaseUp())
        {
            if (playerEnergy - energyDefenderCost < 0)
            {
                CreateTextPlayerEnergy(energyDefenderCost, true);
                return false;
            }
                
            playerEnergy -= energyDefenderCost;
            sliderPlayerEnergy.value = playerEnergy / maxEnergy;
            CreateTextPlayerEnergy(energyDefenderCost, false);
        }
        return true;
    }

    private void CreateTextEnemyEnergy(float cost, bool txtNotEnough)
    {
        GameObject txtTmp = Instantiate(txtEnergy, transTxtEnergy[0].position, Quaternion.identity);
        string strCost = "energy: - " + cost.ToString();

        if(txtNotEnough)
        {
            string str = "Not enough energy";
            txtTmp.GetComponent<TextEnergy>().Init(str, TextEnergy.TColor.Default);
            return;
        }
        txtTmp.GetComponent<TextEnergy>().Init(strCost, TextEnergy.TColor.Enemy);
    }


    private void CreateTextPlayerEnergy(float cost, bool txtNotEnough)
    {
        GameObject txtTmp = Instantiate(txtEnergy, transTxtEnergy[1].position, Quaternion.identity);
        string strCost = "energy: - " + cost.ToString();

        if(txtNotEnough)
        {
            string str = "Not enough energy";
            txtTmp.GetComponent<TextEnergy>().Init(str, TextEnergy.TColor.Default);
            return;
        }
        txtTmp.GetComponent<TextEnergy>().Init(strCost, TextEnergy.TColor.Player);
    }

    
    #endregion

    public static SpawnMgr GetInstance()
    {
        return s_instance;
    }

    public void Reset()
    {
        Destroy(ballTmp);

        //clear the list object 
        foreach (GameObject go in listAttacker)
            if (go != null)
                Destroy(go);
        listAttacker.Clear();

        foreach (GameObject go in listDefender)
            if (go != null)
                Destroy(go);
        listDefender.Clear();

    }

}
