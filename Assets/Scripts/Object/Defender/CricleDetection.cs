using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricleDetection : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Attacker")
        {
            this.GetComponentInParent<Defender>().SetStateMove(other.transform);
        }
    }
}
