using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Defender : MonoBehaviour
{
    [Header("Config param of Attacker")]
    public ScriptDefender scriptDefender;

    [Header("Slider process spaw attacker")]
    public Slider spawnTimeSlider;

    [Header("Chage the effect with state")]
    public GameObject circleDetection;
    public Material matInactive;
    public GameObject shapeRenderer;
    
    [Header("Current state of defender")]
    public State currentState = State.Stand;
    public enum State { Spawning, Stand, Moving, Inactive, ReturnPos, None }

    //
    //private
    //
    private Transform target;

    private float spawnTime = 0;
    private float reactivateTime = 0;
    private float normalSpeed = 0;
    private float returnSpeed = 0;

    private float spawnProcess = 0;
    private float distanceCatch = 1f;

    private Material matDefault;
    private Vector3 originPos;

    //component
    private Animator animator;


    private void LoadData()
    {
        spawnTime = scriptDefender.spawnTime;
        reactivateTime = scriptDefender.reactivateTime;
        normalSpeed = scriptDefender.normalSpeed;
        returnSpeed = scriptDefender.returnSpeed;
        

    }

    #region UNITY
    private void Start()
    {
        LoadData();

        //
        originPos = transform.position;
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
            case State.Spawning:
                OnStateSpawning();
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
    private void OnStateSpawning()
    {
        spawnProcess += Time.deltaTime;
        spawnTimeSlider.value = spawnProcess / spawnTime;

        if (spawnProcess >= spawnTime)
        {
            spawnTimeSlider.gameObject.SetActive(false);
            circleDetection.SetActive(true);

            ChangeState(State.Stand);
        }

    }

    private void OnStateStand()
    {

    }

    private void OnStateMoving()
    {
        if(target == null)
            ChangeState(State.Stand);

        transform.position = Vector3.MoveTowards(transform.position, target.position, normalSpeed * Time.deltaTime);
        
        // catch up the attacker who holding the ball
        if (Vector3.SqrMagnitude(transform.position - target.position) <= distanceCatch * distanceCatch)
        {
            target.GetComponent<Attacker>().PassTheBall();

            // set inactive state up
            EnableStateInactive();
        }
    }

    private void OnStateInactive()
    {

    }

    private void OnStateReturnPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, originPos, returnSpeed * Time.deltaTime);

        if (Vector3.SqrMagnitude(transform.position - originPos) == 0f)
        {
            StartCoroutine("ChangeStateInactiveWithTime", reactivateTime);
        }
    }
    #endregion

    public void EnableStateInactive()
    {
        
        circleDetection.SetActive(false);
        shapeRenderer.GetComponent<Renderer>().material = matInactive;
        
        ChangeState(State.ReturnPos);
    }

    IEnumerator ChangeStateInactiveWithTime(float time)
    {
        SetDefenderInactive();
        ChangeState(State.Inactive);

        yield return new WaitForSeconds(time);
        SetDefenderActive();
        ChangeState(State.Stand);
    }

    public void ChangeState(State newState)
    {
        currentState = newState;
    }

    public void SetDefenderActive()
    {
        animator.SetBool("Inactive", false);
        circleDetection.SetActive(true);
        shapeRenderer.GetComponent<Renderer>().material = matDefault;
    }

    public void SetDefenderInactive()
    {
        animator.SetBool("Inactive", true);
        circleDetection.SetActive(false);
        shapeRenderer.GetComponent<Renderer>().material = matInactive;
    }

    public void SetStateMove(Transform tar)
    {
        target = tar;
        currentState = State.Moving;
    }
}
