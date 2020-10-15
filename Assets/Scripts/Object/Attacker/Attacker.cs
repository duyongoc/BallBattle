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

    [Header("State of Attacker")]
    public State currentState = State.Waiting;
    public enum State { Waiting, Stand, Moving, Inactive, None }


    //
    //private 
    //
    private GameOjbect ball;

    private float normalSpeed = 0;
    private float spawnTime = 0;
    private float spawnProcess = 0;


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
        float distance = Vector3.Distance(transform.position, arrList[0].transform.position);

        foreach (GameObject go in listAttacker)
        {
            var disTmp = Vector3.Distance(transform.position, arrList[0].transform.position);
            if (distance > disTmp)
            {
                distance = disTmp;
                otherAttacker = go;
            }
        }

        ball.GetComponent<Ball>().SetTarget(otherAttacker.transform);
        SetInactivePerTime(4);

        // game over
        if (otherAttacker == null)
        {

        }
    }

    private void SetInactivePerTime(float time)
    {
        ChangeState(State.Stand);
        GetComponent<Collider>().enable = false;
        StartCoroutine("ChangeStateMovingWithTime", time);
    }

    IEnumerator ChangeStateMovingWithTime(float time)
    {
        yield return new WaitforSeconds(time);

        GetComponent<Collider>().enable = true;
        ChangeState(State.Moving);
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
            ball.transform.SetParent(this.transform);
        }
    }



}
