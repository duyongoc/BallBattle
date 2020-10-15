using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Defender : MonoBehaviour
{
    [Header("Config param of Attacker")]
    public ScriptAttacker scriptAttacker;

    [Header("Slider process spaw attacker")]
    public Slider spawnTimeSlider;

    [Header("Current state of defender")]
    public State currentState = State.Stand;
    public enum State { Waiting, Stand, Moving, Inactive, ReturnPos, None }

    //
    //private
    //
    private Transform target;

    private float normalSpeed = 0;
    private float spawnTime = 0;
    private float spawnProcess = 0;

    private float distanceCatch = 0.5f;


    private Vector3 originPos;

    private void LoadData()
    {
        normalSpeed = scriptAttacker.normalSpeed;
        spawnTime = scriptAttacker.spawnTime;

        //
        originPos = transform.position;
    }

    #region UNITY
    private void Start()
    {
        LoadData();
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Waiting:
                OnStateWaiting();
                break;

            case State.Stand:
                OnStateStand();
                break;

            case State.Moving:
                OnStateMoving();
                break;

            case State.Inactive:
                OnStateInactive();
                break;

            case State.ReturnPos:
                OnStateReturnPos();
                break;

            case State.None:
                break;
        }

    }
    #endregion

    #region STATE
    private void OnStateWaiting()
    {
        spawnProcess += Time.deltaTime;
        spawnTimeSlider.value = spawnProcess / spawnTime;

        if (spawnProcess == spawnTime)
        {
            spawnTimeSlider.gameObject.SetActive(false);
            ChangeState(State.Stand);
        }

    }

    private void OnStateStand()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Vector3.forward, 5 * Time.deltaTime);
        //transform.Translate(Vector3.forward * 5 * Time.deltaTime);
    }

    private void OnStateMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, normalSpeed * Time.deltaTime);

        if (Vector3.SqrMagnitude(transform.position - target.position) <= distanceCatch * distanceCatch)
        {
            target.GetComponent<Attacker>().PassTheBall();
            ChangeState(State.Stand);
        }
    }

    private void OnStateInactive()
    {

    }

    private void OnStateReturnPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, originPos, normalSpeed * Time.deltaTime);

        if (Vector3.SqrMagnitude(transform.position - originPos) <= 0.1f)
        {
            //target.GetComponent<Attacker>().PassTheBall();
            ChangeState(State.Stand);
        }
    }
    #endregion

    public void ChangeState(State newState)
    {
        currentState = newState;
    }

    public void SetStateMove(Transform tar)
    {
        target = tar;
        currentState = State.Moving;
    }
}
