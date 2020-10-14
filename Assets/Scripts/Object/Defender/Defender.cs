using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    

    [Header("Current state of defender")]
    public State currentState = State.Idle;
    public enum State { Idle, Moving, None }

    //
    //private
    //
    private Transform target;

    private float moveSpeed = 5f;
    private float distanceCatch = 0.5f;

    #region UNITY
    private void Start()
    {

    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                OnStateIdle();
                break;

            case State.Moving:
                OnStateMoving();
                break;

            case State.None:
                OnStateNone();
                break;
        }

    }
    #endregion

    #region STATE
    private void OnStateIdle()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Vector3.forward, 5 * Time.deltaTime);
        //transform.Translate(Vector3.forward * 5 * Time.deltaTime);
    }

    private void OnStateMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if(Vector3.SqrMagnitude(transform.position - target.position) <= distanceCatch * distanceCatch)
        {
            target.GetComponent<Attacker>().currentState = Attacker.State.None;
            currentState = State.Idle;
        }
    
    }

    private void OnStateNone()
    {

    }
    #endregion

    public void SetStateMove(Transform tar)
    {
        target = tar;
        currentState = State.Moving;
    }
}
