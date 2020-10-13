using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject prefabAttacker;
    public GameObject prefabDefender;

    public void SpawnAttacker(Vector3 position)
    {
        if(LandMgr.GetInstance().IsPhaseDown())
        {
            Instantiate(prefabAttacker, position, Quaternion.Euler(0f, 0f, 0f));
        }
        else if(LandMgr.GetInstance().IsPhaseUp())
        {
            Instantiate(prefabAttacker, position, Quaternion.Euler(0f, 180f, 0f));
        }
    }

    public void SpawnDefender(Vector3 position)
    {
        if(LandMgr.GetInstance().IsPhaseDown())
        {
            Instantiate(prefabDefender, position, Quaternion.Euler(0f, 180f, 0f));
        }
        else if(LandMgr.GetInstance().IsPhaseUp())
        {
            Instantiate(prefabDefender, position, Quaternion.Euler(0f, 0f, 0f));
        }
    }
}
