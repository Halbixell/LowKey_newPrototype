using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowMoveEnemysScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject PopupWithAnimation;


    // Start is called before the first frame update
    void Start()
    {
        PopupWithAnimation.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        PopupWithAnimation.SetActive(true);

    }

    

    public void OnPointerExit(PointerEventData eventData)
    {
        PopupWithAnimation.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
