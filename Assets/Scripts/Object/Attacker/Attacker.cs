using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacker : MonoBehaviour
{

    [Header("Config param of Attacker")]
    public ScriptAttacker scriptAttacker;

    [Header("Slider process spaw attacker")]
    public Slider spawnTimeSlider;

    [Header("Position to hold the ball")]
    public Transform transHoldBall;

    [Header("State of Attacker")]
    public State currentState = State.Waiting;
    public enum State { Waiting, Stand, Moving, Inactive, None }

    
    //
    //private 
    //
    private GameObject ball = null;

    private float normalSpeed = 0;
    private float spawnTime = 0;
    private float spawnProcess = 0;

    private bool inActive = false;
    public bool InActive { get => inActive; set => inActive = value; }

    private void LoadData()
    {
        normalSpeed = scriptAttacker.normalSpeed;
        spawnTime = scriptAttacker.spawnTime;
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

        if (spawnProcess >= spawnTime)
        {
            spawnTimeSlider.gameObject.SetActive(false);
            spawnProcess = 0;

            ChangeState(State.Moving);
        }
    }

    private void OnStateStand()
    {

    }

    private void OnStateMoving()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Vector3.forward, 5 * Time.deltaTime);
        transform.Translate(Vector3.forward * normalSpeed * Time.deltaTime);

    }

    private void OnStateInactive()
    {

    }
    #endregion

    public void PassTheBall()
    {
        GameObject otherAttacker = null;
        var listAttacker = SpawnMgr.GetInstance().listAttacker;
        
        float distance = 100;
        foreach (GameObject go in listAttacker)
        {
            if(go.GetComponent<Attacker>().StateInactive())
                continue;

            var disTmp = Vector3.Distance(transform.position, go.transform.position);
            if (disTmp != 0 && disTmp < distance )
            {
                distance = disTmp;
                otherAttacker = go;
            }
        }

        if(otherAttacker != null && ball != null)
        {
            ball.GetComponent<Ball>().SetTarget(otherAttacker.transform);
            ball = null;
        }
            
        SetInactivePerTime(3);

        // game over
        if (otherAttacker == null)
        {

        }
    }

    private void SetInactivePerTime(float time)
    {
        ChangeState(State.Inactive);
        GetComponent<Collider>().enabled = false;
    
        StartCoroutine("ChangeStateMovingWithTime", time);
    }

    public bool IsHoldTheBall()
    {
        return (ball != null);
    }

    public void CatchTheBall(GameObject obj)
    {
        ball = obj.gameObject;
        ball.transform.position = transHoldBall.position;
        ball.transform.SetParent(this.transform);
    }

    IEnumerator ChangeStateMovingWithTime(float time)
    {
        yield return new WaitForSeconds(time);

        GetComponent<Collider>().enabled = true;
        ChangeState(State.Moving);
    }

    public bool StateInactive()
    {
        return currentState == State.Inactive;
    }

    public void ChangeState(State newState)
    {
        currentState = newState;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            ball = other.gameObject;
            ball.transform.position = transHoldBall.position;
            ball.transform.SetParent(this.transform);
        }
    }



}
