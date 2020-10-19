using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SceneMaze : StateScene
{
    [Header("Random Ball")]
    public GameObject ball;
    public float maxRangeSpawn;
    
    [Header("Object")]
    public GameObject enemyPanel;
    public GameObject playerPanel;
    public GameObject aRMode;
    public GameObject landObject;
    public GameObject mazeObject;


    #region SCENE
    public override void StartState()
    {
        base.StartState();

        SpawnMgr.GetInstance().Reset();
        Owner.SetActivePanelScene(this.name);

        enemyPanel.SetActive(false);
        playerPanel.SetActive(false);
        landObject.SetActive(false);
        aRMode.SetActive(false);

        mazeObject.SetActive(true);
        SetRandomBallPos();
    }

    public override void UpdateState()
    {
        base.UpdateState();

    }

    public override void EndState()
    {
        base.EndState();

        enemyPanel.SetActive(true);
        playerPanel.SetActive(true);
        aRMode.SetActive(true);
        
    }
    #endregion

    private void SetRandomBallPos()
    {
        Vector3 vec;
        NavMeshHit hit;
        do
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxRangeSpawn;
            NavMesh.SamplePosition(randomDirection, out hit, maxRangeSpawn, 1);
            vec = new Vector3(hit.position.x, 0, hit.position.z);
        }
        // check the point was created isn't near player which range minRangeSpawn
        while ( vec.x < -8 || vec.x >= 8 || vec.z < -15.5f || vec.z > 15.5f
            && (hit.position.x == Mathf.Infinity && hit.position.y == Mathf.Infinity)
        );

        //Debug.Log(Vector3.Distance(hit.position,target.position) + " / " + minRangeSpawn);
        ball.transform.position = vec;
    }


}
