using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    [Header("All of scene in game")]
    public SceneMenu m_sceneMenu;
    public SceneInGame m_sceneInGame;
    public SceneEndPhase m_sceneEndPhase;
    public SceneEndGame m_sceneEndGame;

    [Header("current state of scene")]
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
        //ChangeState(m_sceneMenu);
        ChangeState(m_sceneInGame);
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
        // m_scseneInGame.gameObject.SetActive(panelName.Contains(m_sceneInGame.name));
        m_sceneMenu.gameObject.SetActive(panelName.Contains(m_sceneMenu.name));
        m_sceneEndPhase.gameObject.SetActive(panelName.Contains(m_sceneEndPhase.name));
        m_sceneEndGame.gameObject.SetActive(panelName.Contains(m_sceneEndGame.name));
    }

    public static SceneMgr GetInstance()
    {
        return s_instance;
    }

    public void EndOfPhase()
    {

    }

    public void EndOfGame()
    {

    }

    // public bool IsStateTutorial()
    // {
    //     return currentState == m_sceneTutorial;
    // }

}
