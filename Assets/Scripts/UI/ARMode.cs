using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ARMode : MonoBehaviour, IPointerDownHandler
{

    public Slider btnAR;

    private void Start()
    {
        // DontDestroyOnLoad(this);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
        Debug.Log("Clickedddd: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(btnAR.value < 1)
        {
            btnAR.value = 1;
            SceneManager.LoadScene(1);
        }
        else if(btnAR.value == 1)
        {
            btnAR.value = 0;
            SceneManager.LoadScene(0);
        }
        // Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }
}
