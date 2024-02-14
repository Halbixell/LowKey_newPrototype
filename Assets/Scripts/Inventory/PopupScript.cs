using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject popup;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private IndexMemory indexMemory;
    private GameObject diesesBild;


    public enum Direction
    {
        Right = 0,
        Up = 1,
        Left = 2,
        Down = 3
    }



    public void Start()
    {
        popup.SetActive(false);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        popup.SetActive(true);
        Transform child = inventoryManager.moveSlots[indexMemory.IndexLastMove - 1].transform.GetChild(0);

        Sprite[] Liste = inventoryManager.PictureList;
        GameObject[] PictureOfMove_Liste = inventoryManager.PictureOfMoves;
        Image image = child.GetComponent<Image>();
        //zeige das richtige Bild
        for (int i = 0; i < Liste.Length; i++)
        {
            if (image.sprite == Liste[i])
            {
                diesesBild = PictureOfMove_Liste[i];
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
        InventorySlot slot = inventoryManager.moveSlots[indexMemory.IndexLastMove - 1];
        slot.transform.GetChild(0).GetComponent<DraggableItem>().item.ChangeDirection((int) (slot.transform.GetChild(0).GetComponent<DraggableItem>().item.Dir+4 + 1) % 4);
        slot.SelectedMove = slot.transform.GetChild(0).GetComponent<DraggableItem>().item;
    }

    public void OnButtonRight()
    {

        InventorySlot slot = inventoryManager.moveSlots[indexMemory.IndexLastMove - 1];
        slot.transform.GetChild(0).GetComponent<DraggableItem>().item.ChangeDirection((int) (slot.transform.GetChild(0).GetComponent<DraggableItem>().item.Dir +4-1)%4);

        slot.SelectedMove = slot.transform.GetChild(0).GetComponent<DraggableItem>().item;
        Debug.Log(slot.SelectedMove.Dir);

    }

    //Funktionen für die Buttons: Links: Drehe das Bild nach links und setze Wert vom Move aus den gedrehten wert
    //Rechts: drehe Bild nach Rechts

    //Generell: Beim drüberfahren: Nimm Bild so wie die richtung vom Move ist


}
