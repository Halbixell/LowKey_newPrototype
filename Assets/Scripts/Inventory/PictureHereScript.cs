using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PictureHereScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject PictureOfMove;

    public void Start()
    {
        PictureOfMove.SetActive(false);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PictureOfMove.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        PictureOfMove.SetActive(true);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    { 
        PictureOfMove.SetActive(false);
        
    }
}
