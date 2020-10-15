using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Finder : MonoBehaviour
{

    public Transform target;

    //
    // private variable
    //
    private NavMeshAgent navMeshAgent;
    
    #region UNITY
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        navMeshAgent.destination = target.position;
    }
    #endregion
}
