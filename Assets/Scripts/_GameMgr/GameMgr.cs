using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    
    // public bool isSkipTutorial;
    // public bool isMovingCamera;

    #region singleton
    public static GameMgr s_instane; 

    private void Awake()
    {
        if(s_instane != null)
        {
            return;
        }
        
        LoadData();
        s_instane = this;

        return;
    }
    #endregion  

    public void LoadData()
    {

    }

    

    public static GameMgr GetInstance()
    {
        return s_instane;
    }
}
