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
    }
    #endregion

    #region STATE
    private void OnStateMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void OnStateNone()
    {

    }
    #endregion

    public void SetTarget(Transform tar)
    {
        target = tar;
    }

}

