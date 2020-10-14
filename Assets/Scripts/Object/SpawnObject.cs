using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [Header("Object")]
    public GameObject prefabAttacker;
    public GameObject prefabDefender;

    [Header("The Ball")]
    public GameObject prefabBall;

    public List<GameObject> listObject;

    //
    //private variable
    //
    private GameObject tmp; 
    private GameObject ballTmp;

    

    public void CreateTheBall()
    {
        if(LandMgr.GetInstance().IsPhaseDown())
        {
            float randX = Random.Range(-7.5f, 7.5f);
            float randZ = Random.Range(-12f, 0f);

            ballTmp = Instantiate(prefabBall, new Vector3(randX, 0f, randZ), Quaternion.identity);
        }
        else if(LandMgr.GetInstance().IsPhaseUp())
        {
            float randX = Random.Range(-7.5f, 7.5f);
            float randZ = Random.Range(0f, 12f);

            ballTmp = Instantiate(prefabBall, new Vector3(randX, 0f, randZ), Quaternion.identity);
        }
    }

    public void SpawnAttacker(Vector3 position)
    {
        if(LandMgr.GetInstance().IsPhaseDown())
        {
            tmp = Instantiate(prefabAttacker, position, Quaternion.Euler(0f, 0f, 0f));
        }
        else if(LandMgr.GetInstance().IsPhaseUp())
        {
            tmp = Instantiate(prefabAttacker, position, Quaternion.Euler(0f, 180f, 0f));
        }

        listObject.Add(tmp);
    }

    public void SpawnDefender(Vector3 position)
    {
        if(LandMgr.GetInstance().IsPhaseDown())
        {
            tmp = Instantiate(prefabDefender, position, Quaternion.Euler(0f, 180f, 0f));
        }
        else if(LandMgr.GetInstance().IsPhaseUp())
        {
            tmp = Instantiate(prefabDefender, position, Quaternion.Euler(0f, 0f, 0f));
        }

        listObject.Add(tmp);
    }


    public void Reset()
    {
        Destroy(ballTmp);

        foreach(GameObject go in listObject)
        {
            if(go != null)
                Destroy(go);
        }
        listObject.Clear();
        
    }

}
