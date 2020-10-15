using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricleDetection : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Attacker")
        {
            // check the attacker isn't holding the ball
            var tar = other.GetComponent<Attacker>();
            if(!tar.IsHoldTheBall())
                return;

            this.GetComponentInParent<Defender>().SetStateMove(other.transform);
        }
    }
}
