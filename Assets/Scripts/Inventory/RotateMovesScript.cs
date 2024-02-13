using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotateMovesScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InventoryManager inventoryManager;

    //public float popupOffsetY = 50f; // Anpassbarer Offset
    private RectTransform popupRectTransform;

    public GameObject popup;
    
    

    public void Start()
    {
        popupRectTransform = popup.GetComponent<RectTransform>();
        if (popup != null)
        {
            popup.SetActive(false);
        }

        GameObject[] PictureOfMove_Liste = inventoryManager.PictureOfMoves;

        for (int i = 0; i < PictureOfMove_Liste.Length; i++)
        {
            PictureOfMove_Liste[i].SetActive(false);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Ich bin über dem MoveSlot");

        GameObject[] PictureOfMove_Liste = inventoryManager.PictureOfMoves;

        if (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                Sprite[] Liste = inventoryManager.PictureList;
                for(int i=0;i< Liste.Length; i++)
                {
                    if (image.sprite == Liste[i])
                    {
                        //Position von Popup setzen
                        float slotHeight = GetComponent<RectTransform>().rect.height;
                        Vector3 popupPosition = transform.position + new Vector3(0f, 1.5f*slotHeight, 0f);
                        popupRectTransform.position = popupPosition;


                        //Bild von Move aktivieren
                        PictureOfMove_Liste[i].SetActive(true);

                        if (popup != null)
                        {
                            popup.SetActive(true);
                        }
           
                    }
                }
            }
           
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bool isPointerOverSlot = EventSystem.current.IsPointerOverGameObject();
        bool isPointerOverPopup = EventSystem.current.currentSelectedGameObject == popup;


        if (!isPointerOverSlot|| (!isPointerOverPopup && EventSystem.current.IsPointerOverGameObject()))
        {
           
            GameObject[] PictureOfMove_Liste = inventoryManager.PictureOfMoves;

            for (int i = 0; i < PictureOfMove_Liste.Length; i++)
            {
                PictureOfMove_Liste[i].SetActive(false);
            }


            if (popup != null)
            {
                popup.SetActive(false);
            }
        }
        
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
