using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInGame : StateScene
{
    [Header("Object")]
    public GameObject landObject;
    public GameObject mazeObject;

    public AudioClip ingameAudio;
    

    public override void StartState()
    {
        base.EndState();
        Owner.SetActivePanelScene(this.name);
        
        landObject.SetActive(true);
        mazeObject.SetActive(false);

        //sound
        SoundMgr.GetInstance().StopSound();
        SoundMgr.GetInstance().PlaySoundOneShot(ingameAudio);
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
