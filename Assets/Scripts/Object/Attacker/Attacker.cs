using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

    [Header("Move speed of Attacker")]
    public float moveSpeed = 5f;

    [Header("State of Attacker")]
    public State currentState = State.Move;
    public enum State { Move, None}
    

    #region UNITY
    private void Start()
    {

    }   

    private void Update()
    {
        switch(currentState)
        {
            case State.Move:
            OnStateMove();
            break;

            case State.None:
            OnStateNone();
            break;
        }

    }
    #endregion

    #region STATE
    private void OnStateMove()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Vector3.forward, 5 * Time.deltaTime);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnStateNone()
    {

    }
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            other.transform.SetParent(this.transform);
        }
    }

}
