using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandMgr : MonoBehaviour
{
    [Header("Set up Config game")]
    public ScriptConfig scriptConfig;
    
    [Header("Show text time up")]
    public Text txtTime;

    [Header("Current phase")]
    public Phase currentPhase = Phase.DOWN;
    public enum Phase { DOWN, UP };

    //
    //private
    //
    private int layer_mask;

    private float timeLimit = 0;
    private float timer = 0;

    //
    private bool firstCreateBall = false;

    private SpawnMgr spawnMgr;

    #region singleton
    public static LandMgr s_instance;
    
    private void Awake()
    {
        if(s_instance != null)
            return;
        
        s_instance = this;
    }
    #endregion

    private void LoadData()
    {
        timer = timeLimit = scriptConfig.timeLimit;
    }

    #region UNITY
    private void Start()
    {
        layer_mask = LayerMask.GetMask("Ground");
        spawnMgr = SpawnMgr.GetInstance();

        LoadData();

    }

    private void Update()
    {
        UpdatePhase();
        switch (currentPhase)
        {
            case Phase.DOWN:
                OnUpdatePhaseDown();

                break;
            case Phase.UP:
                OnUpdatePhaseUp();

                break;
        }
        
    }
    #endregion

    #region Update PHASE
    private void UpdatePhase()
    {
        txtTime.text = ((int)timer).ToString();
        timer -= Time.deltaTime;

        if(timer <= 0)
            ChangePhaseWithTime();
    }

    private void OnUpdatePhaseDown()
    {
        CreateTheBall();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, layer_mask))
            {
                if(hit.point.z < 0)
                {
                    spawnMgr.SpawnAttacker(hit.point);
                }
                else if(hit.point.z > 0)
                {
                    spawnMgr.SpawnDefender(hit.point);
                }
                // Debug.Log(hit.point.z);
            }
        }
    }

    private void OnUpdatePhaseUp()
    {
        CreateTheBall();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, layer_mask))
            {
                if(hit.point.z < 0)
                {
                    spawnMgr.SpawnDefender(hit.point);
                }
                else if(hit.point.z > 0)
                {
                    spawnMgr.SpawnAttacker(hit.point);
                }
                // Debug.Log(hit.point.z);
            }
        }
    }
    #endregion

    private void ChangePhaseWithTime()
    {
        if(IsPhaseDown())
            currentPhase = Phase.UP;
        else
            currentPhase = Phase.DOWN;

        ResetPhase();
        timer = timeLimit;
    }

    private void CreateTheBall()
    {
        if(!firstCreateBall)
        {
            spawnMgr.CreateTheBall();
            firstCreateBall = true;
        }
    }

    private void ResetPhase()
    {
        firstCreateBall = false;
        spawnMgr.Reset();

    }

    public static LandMgr GetInstance()
    {
        return s_instance;
    }

    public bool IsPhaseDown()
    {
        return currentPhase == Phase.DOWN;
    }
    public bool IsPhaseUp()
    {
        return currentPhase == Phase.UP;
    }
}
