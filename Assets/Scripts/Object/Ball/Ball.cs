using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [Header("Current state of defender")]
    public State currentState = State.None;
    public enum State { Moving, None }

    //
    //private
    //
    private Transform target;
    private float moveSpeed = 5f;

    #region UNITY
    private void Start()
    {
        
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                OnStateMoving();
                break;

            case State.None:
                OnStateNone();
                break;
        }
        // Debug.Log(currentState);
    }
    #endregion

    #region STATE
    private void OnStateMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target.position) <= 1.5f)
        {
            var tar = target.GetComponent<Attacker>();
            tar.CatchUpTheBall(this.gameObject);

            ChangeState(State.None);
        }
    }

    private void OnStateNone()
    {
        
    }
    #endregion

    public void SetTarget(Transform tar)
    {
        target = tar;
        ChangeState(State.Moving);
    }

    public void ChangeState(State newState)
    {
        currentState = newState;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TheWall")
        {
            GetComponentInParent<Attacker>().RemoveChild(this.gameObject);
            this.transform.parent = null;

            SpawnMgr.GetInstance().SetBallRandomPosition(this.transform);
        }
    }


}

