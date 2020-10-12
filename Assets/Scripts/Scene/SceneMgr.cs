using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    [Header("All of scene in game")]
    public SceneMenu m_sceneMenu;
    public SceneInGame m_sceneInGame;
    public SceneGameOver m_sceneGameOver;
    //current state scene
    private StateScene currentState; 
    public StateScene CurrentState { get => currentState; set => currentState = value; }

    #region Init
    public static SceneMgr s_instance;
    private void Awake()
    {
        if(s_instance != null)
            return;
        s_instance = this;

    }
    #endregion


    #region UNITY
    private void Start()
    {

        ChangeState(m_sceneMenu);
    }

    private void FixedUpdate()
    {
        if(CurrentState != null)
        {
            CurrentState.UpdateState();
        }
    }
    #endregion


    public void ChangeState(StateScene newState)
    {
        if(currentState != null)
        {
            currentState.EndState();
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.Owner = this;
            currentState.StartState();
        }
    }

    public void SetActivePanelScene(string panelName)
    {
        m_sceneMenu.gameObject.SetActive(panelName.Contains(m_sceneMenu.name));
        m_sceneInGame.gameObject.SetActive(panelName.Contains(m_sceneInGame.name));
        m_sceneGameOver.gameObject.SetActive(panelName.Contains(m_sceneGameOver.name));
    }

    public static SceneMgr GetInstance()
    {
        return s_instance;
    }

    public bool IsStateInGame()
    {
        return false;//m_sceneInGame.isPlaying;
    }

    // public bool IsStateTutorial()
    // {
    //     return currentState == m_sceneTutorial;
    // }

}
