using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{

    [Header("Phase game")]
    public int amountPhaseGame;
    public int enemyWinPhase;
    public int playerWinPhase;
    


    //
    //  private variable
    //
    private SceneMgr sceneMgr;

    private int currentPhaseGame;


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

    #region UNITY
    private void Start()
    {
        LoadData();

        // get component
        sceneMgr = SceneMgr.GetInstance();
    }

    private void Update()
    {
        if(currentPhaseGame == amountPhaseGame)
        {
            FinishPhaseGame();
        }
    }
    #endregion

    public void SetPhaseDraw()
    {
        currentPhaseGame++;
        sceneMgr.m_sceneEndPhase.Init("", "Draw");
        sceneMgr.ChangeState(sceneMgr.m_sceneEndPhase);
    }
    
    public void SetPhaseEnemywin()
    {
        currentPhaseGame++;
        enemyWinPhase++;
        sceneMgr.m_sceneEndPhase.Init("Enemy", "Win");
        sceneMgr.ChangeState(sceneMgr.m_sceneEndPhase);
    }

    public void SetPhasePlayerWin()
    {
        currentPhaseGame++;
        playerWinPhase++;
        sceneMgr.m_sceneEndPhase.Init("Player", "Win");
        sceneMgr.ChangeState(sceneMgr.m_sceneEndPhase);
    }

    public void CheckPhaseWinOfAttacker()
    {
        // attacker spawning in phase down is player -> enemy win
        if (LandMgr.GetInstance().IsPhaseDown())
        {
            SetPhaseEnemywin();
        }
        // attacker spawning in phase up is enemy -> player win
        if (LandMgr.GetInstance().IsPhaseUp())
        {
            SetPhasePlayerWin();
        }
    }

    public void CheckPhaseWinInGate(bool hasBall)
    {
        // Gate: attacker spawning in phase down is player -> player win
        if (LandMgr.GetInstance().IsPhaseDown() && hasBall)
        {
            SetPhasePlayerWin();
        }
        // Gate: attacker spawning in phase up is enemy -> enemy win
        if (LandMgr.GetInstance().IsPhaseUp() && hasBall)
        {
            SetPhaseEnemywin();
        }
    }

    public void FinishGameMaze(bool hasBall)
    {

        if (hasBall)
        {
            sceneMgr.m_sceneEndGame.InitMaze("Finish Maze", "Player ", "Win");
            sceneMgr.ChangeState(sceneMgr.m_sceneEndGame);
        }
        else
        {
            sceneMgr.m_sceneEndGame.InitMaze("Can't get the ball. Finish Maze", "Enemy ", "Win");
            sceneMgr.ChangeState(sceneMgr.m_sceneEndGame);
        }
    }


    public void FinishPhaseGame()
    {   

        sceneMgr.m_sceneEndGame.Init(enemyWinPhase, playerWinPhase, "Win");

        // sceneMgr.m_sceneEndGame.Init("Drawn", "You must go to Penatly");

        sceneMgr.ChangeState(sceneMgr.m_sceneEndGame);
        currentPhaseGame = 0;
    }


    public bool IsStateInGame()
    {
        return (sceneMgr.CurrentState == sceneMgr.m_sceneInGame);
    }

    public static GameMgr GetInstance()
    {
        return s_instane;
    }

    public void Reset()
    {
        enemyWinPhase = 0;
        playerWinPhase = 0;

    }
}
