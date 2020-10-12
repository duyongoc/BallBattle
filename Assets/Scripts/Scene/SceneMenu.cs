using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMenu : StateScene
{

    #region SCENE
    public override void StartState()
    {
        base.StartState();
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
    #endregion


    #region Handler events of button
    public void OnPressButtonPlay()
    {
        Owner.ChangeState(Owner.m_sceneInGame);
    }

    public void OnPressButtonExit()
    {
        Application.Quit();
    }
    
    #endregion
}
