using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneEndGame : StateScene
{
    [Header("Param")]
    public Text txtName;
    public Text txtStatus;

    [Header("Button text")]
    public Text txtBtnOK;

    [Header("Object")]
    public GameObject landObject;
    public GameObject mazeObject;

    [Header("Finder")]
    public Finder finder;

    //
    //  private
    //
    private bool makePenatly = false;

    public void Init(int numEneny, int numPlayer, string status)
    {
        string strName = numPlayer > numEneny ? "Player" : "Enemy";
        txtName.text = "Enemy: " + numEneny + "\n";
        txtName.text += "Player: " + numPlayer;

        txtStatus.text = strName + " " + status;

        if(numPlayer == numEneny)
        {
            makePenatly = true;
            txtStatus.text = "Game Draw";
            txtBtnOK.text = "Penatly";
        }
    }

    public void InitMaze(string intro, string name, string status)
    {
        txtName.text = intro;
        txtStatus.text = name + " " + status;
    }
    
    #region UNITY
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

        txtBtnOK.text = "OK";
        makePenatly = false;

        landObject.SetActive(true);
        mazeObject.SetActive(false);

    }
    #endregion

    #region Events of button
    public void OnPressButtonOK()
    {
        Reset();

        if(!makePenatly)
        {
            Owner.ChangeState(Owner.m_sceneInGame);
        }
        else
        {
            Owner.ChangeState(Owner.m_sceneMaze);
        }
        
    }
    #endregion

    private void Reset()
    {   
        // reset Mgr
        GameMgr.GetInstance().Reset();
        LandMgr.GetInstance().Reset();
        SpawnMgr.GetInstance().ResetEnergy();
        finder.Reset();
    }
    
}
