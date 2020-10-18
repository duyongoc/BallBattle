﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacker : MonoBehaviour
{

    [Header("Config param of Attacker")]
    public ScriptAttacker scriptAttacker;

    [Header("Slider process spaw attacker")]
    public Slider spawnTimeSlider;

    [Header("Chage the effect with state")]
    public Material matInactive;
    public GameObject shapeRenderer;

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
    private float reactivateTime = 0;
    private float spawnTime = 0;
    private float spawnProcess = 0;

    private Material matDefault;

    //component
    private Animator animator;


    private void LoadData()
    {
        normalSpeed = scriptAttacker.normalSpeed;
        reactivateTime = scriptAttacker.reactivateTime;
        spawnTime = scriptAttacker.spawnTime;
        
    }

    #region UNITY
    private void Start()
    {
        LoadData();

        //
        matDefault = shapeRenderer.GetComponent<Renderer>().material;

        //get component
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!GameMgr.GetInstance().IsStateInGame())
            return;

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
            var atk = go.GetComponent<Attacker>();
            if(atk.CheckStateInactive())
                continue;

            var disTmp = Vector3.Distance(transform.position, go.transform.position);
            if (disTmp != 0 && disTmp < distance && atk.CheckValidPostion(atk.transform))
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
        StartCoroutine("ChangeStateInactiveWithTime", reactivateTime);

        if (otherAttacker == null)
        {
            GameMgr.GetInstance().CheckPhaseWinOfAttacker();
        }
    }

    public bool IsHoldTheBall()
    {
        return (ball != null);
    }

    public void CatchUpTheBall(GameObject obj)
    {
        ball = obj.gameObject;
        ball.transform.position = transHoldBall.position;
        ball.transform.SetParent(this.transform);
    }

    IEnumerator ChangeStateInactiveWithTime(float time)
    {
        SetAttackerInactive();
        ChangeState(State.Inactive);

        yield return new WaitForSeconds(time);

        SetAttackerActive();
        ChangeState(State.Moving);
    }

    public void SetAttackerActive()
    {
        animator.SetBool("Inactive", false);
        shapeRenderer.GetComponent<Renderer>().material = matDefault;
        this.GetComponent<Collider>().enabled = true;
    }

    public void SetAttackerInactive()
    {
        animator.SetBool("Inactive", true);
        shapeRenderer.GetComponent<Renderer>().material = matInactive;
        this.GetComponent<Collider>().enabled = false;
    }

    public bool CheckValidPostion(Transform tran)
    {
        if(LandMgr.GetInstance().IsPhaseDown())
        {
            return (tran.position.z < 12);
        }
        if(LandMgr.GetInstance().IsPhaseUp())
        {
            return (tran.position.z > -12);
        }

        return false;
    }

    public bool CheckStateInactive()
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
