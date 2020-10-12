using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneGameOver : StateScene
{

    public override void StartState()
    {
        base.EndState();
        Owner.SetActivePanelScene(this.name);

        //sound
        // SoundMgr.GetInstance().StopSound();
        // SoundMgr.GetInstance().PlaySoundOneShot(m_audioEnd);
    }

    public override void UpdateState()
    {
        base.UpdateState();

    }

    public override void EndState()
    {
        base.EndState();

    }

    #region Events of button
    public void OnPressButtonReplay()
    {
        Reset();
        Owner.ChangeState(Owner.m_sceneInGame);
        
    }

    public void OnPressButtonMenu()
    {
        Reset();
        owner.ChangeState(Owner.m_sceneMenu);
    }

    public void OnPressButtonExit()
    {
        Application.Quit();

    }
    #endregion

    private void Reset()
    {   
        // reset Mgr
        
    }
}
