using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    public State currentState = State.Defend;
    public enum State { Defend, None}

    #region UNITY
    private void Start()
    {

    }   

    private void FixedUpdate()
    {
        switch(currentState)
        {
            case State.Defend:
            OnStateDefend();
            break;

            case State.None:
            OnStateNone();
            break;
        }

    }
    #endregion

    #region STATE
    private void OnStateDefend()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Vector3.forward, 5 * Time.deltaTime);
        //transform.Translate(Vector3.forward * 5 * Time.deltaTime);
    }

    private void OnStateNone()
    {

    }
    #endregion
}
