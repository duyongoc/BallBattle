using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Finder : MonoBehaviour
{
    [Header("Target position")]
    public Transform target;

    [Header("Position to hold the ball")]
    public Transform transHoldBall;
    private GameObject ball = null;

    //
    // private variable
    //
    private NavMeshAgent navMeshAgent;
    
    #region UNITY
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        navMeshAgent.destination = target.position;
    }
    #endregion

    public void Reset()
    {
        ball = null;
        transform.position = new Vector3(0, 0, -15.5f);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            ball = other.gameObject;
            ball.transform.position = transHoldBall.position;
            ball.transform.SetParent(this.transform);
        }
        else if(other.tag == "Gate")
        {
            GameMgr.GetInstance().FinishGameMaze(ball != null);
        }
    }
}
