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
        PictureOfMove.SetActive(true);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    { 
        PictureOfMove.SetActive(false);
        
    }
}
