﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInGame : StateScene
{
    [Header("Tap to play in scene in game")]
    public GameObject textTapToPlay;
    

    public override void StartState()
    {
        base.EndState();
        Owner.SetActivePanelScene(this.name);
        
    }

    public override void UpdateState()
    {
        base.UpdateState();

        
    }

    public override void EndState()
    {
        base.EndState();

    }

    #region Handler event of button
    public void OnPressButtonPauseGame()
    {
        //Owner.ChangeState(Owner.m_pauseGameScene);
    }
    #endregion

    
}