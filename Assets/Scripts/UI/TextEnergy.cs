using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEnergy : MonoBehaviour
{
    [Header("Data")]
    public float timeDestroy = 3;
    public float moveSpeed = 5;

    [Header("Text")]
    public Text txtEnergy;
    public enum TColor {Player, Enemy, Default };
    
    public void Init(string textValue, TColor tColor = TColor.Default)
    {
        switch(tColor)
        {
            case TColor.Enemy:
                txtEnergy.color = Color.red;
                break;
            case TColor.Player:
                txtEnergy.color = Color.blue;
                break;
            case TColor.Default:
                txtEnergy.color = Color.white;
                break;
        }
        txtEnergy.text = textValue;
    }

    #region UNITY
    private void Start()
    {
        Destroy(this.gameObject, timeDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
    #endregion

}
