using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMgr : MonoBehaviour
{
    [Header("Param of the game")]
    public ScriptAttacker scriptAttacker;
    public ScriptDefender scriptDefender;

    [Header("All of energy of the game")]
    public float enemyEnergy;
    public float playerEnergy;
    public float delayAmount = 1;
    
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

    #region singleton
    public static SpawnMgr s_instance;

    private void Awake()
    {
        if(s_instance != null)
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
    }

    #region UNITY
    private void Start()
    {

    }

    private void FixedUpdate()
    {   
        timer += Time.deltaTime;
        if(timer >= delayAmount) 
        {
            enemyEnergy += energyRegenEnemy;
            playerEnergy += energyRegenPlayer;

            timer = 0;
        }
    }
    #endregion


    #region CREATE OBJECT
    public void CreateTheBall()
    {
        if(LandMgr.GetInstance().IsPhaseDown())
        {
            float randX = Random.Range(-7.5f, 7.5f);
            float randZ = Random.Range(-12f, 0f);

            ballTmp = Instantiate(prefabBall, new Vector3(randX, 0f, randZ), Quaternion.identity);
        }
        else if(LandMgr.GetInstance().IsPhaseUp())
        {
            float randX = Random.Range(-7.5f, 7.5f);
            float randZ = Random.Range(0f, 12f);

            ballTmp = Instantiate(prefabBall, new Vector3(randX, 0f, randZ), Quaternion.identity);
        }
    }

    public void SpawnAttacker(Vector3 position)
    {
        if(LandMgr.GetInstance().IsPhaseDown())
        {
            tmp = Instantiate(prefabAttacker, position, Quaternion.Euler(0f, 0f, 0f));
        }
        else if(LandMgr.GetInstance().IsPhaseUp())
        {
            tmp = Instantiate(prefabAttacker, position, Quaternion.Euler(0f, 180f, 0f));
        }

        listAttacker.Add(tmp);
    }

    public void SpawnDefender(Vector3 position)
    {
        if(LandMgr.GetInstance().IsPhaseDown())
        {
            tmp = Instantiate(prefabDefender, position, Quaternion.Euler(0f, 180f, 0f));
        }
        else if(LandMgr.GetInstance().IsPhaseUp())
        {
            tmp = Instantiate(prefabDefender, position, Quaternion.Euler(0f, 0f, 0f));
        }

        listDefender.Add(tmp);
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
        foreach(GameObject go in listAttacker)
            if(go != null)
                Destroy(go);
        listAttacker.Clear();

        foreach(GameObject go in listDefender)
            if(go != null)
                Destroy(go);
        listDefender.Clear();
        
    }

}
