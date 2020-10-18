using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneEndPhase : StateScene
{
    [Header("Param")]
    public Text txtName;
    public Text txtStatus;


    public void Init(string name, string status)
    {
        txtName.text = name;
        txtStatus.text = status;
    }

    #region UNITY
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
    #endregion

    #region Events of button
    public void OnPressButtonOK()
    {
        Reset();

        LandMgr.GetInstance().ChangePhaseGame();
        Owner.ChangeState(Owner.m_sceneInGame);
    }

    #endregion

    private void Reset()
    {   
        // reset Mgr
        
    }


}
