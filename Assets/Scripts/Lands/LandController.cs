using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandController : MonoBehaviour
{
    
    public GameObject myPrefab;

    #region UNITY
    private void Start()
    {


    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
            {
                if (hit.collider.name == "--LandController--")
                    Instantiate(myPrefab, hit.point, Quaternion.identity);
            }
        }
    }
    #endregion
}
