using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject popup;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private RotateMovesScript rotateMoveScript;

    public void Start()
    {
        popup.SetActive(false);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        popup.SetActive(true);

        Transform child = rotateMoveScript.child;
        Sprite[] Liste = inventoryManager.PictureList;
        GameObject[] PictureOfMove_Liste = inventoryManager.PictureOfMoves;
        Image image = child.GetComponent<Image>();
        //zeige das richtige Bild
        for (int i = 0; i < Liste.Length; i++)
        {
            if (image.sprite == Liste[i])
            {
                //Bild von Move aktivieren
                PictureOfMove_Liste[i].SetActive(true);
            }
        }

        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popup.SetActive(false);
        GameObject[] PictureOfMove_Liste = inventoryManager.PictureOfMoves;
        //setze alle Bilder aus
        for (int i = 0; i < PictureOfMove_Liste.Length; i++)
        {
            PictureOfMove_Liste[i].SetActive(false);
            
        }



    }

    public void OnButtonLeft()
    {

    }

    public void OnButtonRight()
    {

    }

    //Funktionen für die Buttons: Links: Drehe das Bild nach links und setze Wert vom Move aus den gedrehten wert
    //Rechts: drehe Bild nach Rechts

    //Generell: Beim drüberfahren: Nimm Bild so wie die richtung vom Move ist


}
