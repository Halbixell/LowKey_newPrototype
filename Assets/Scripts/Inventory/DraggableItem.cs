
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Item;


public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Text counttext;
    //public int ItemIndex;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int count=1;
    [HideInInspector] public int countspeicher = 1;
    [HideInInspector]public Transform startParent;
    [HideInInspector]public Item item;
    [HideInInspector] DraggableItem draggedItemScript;

    [SerializeField] private InventoryManager _inventoryManager;

    private GameObject draggedItem;

    private Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.isKinematic = true;
        _inventoryManager = FindObjectOfType<InventoryManager>();
    }

    
    public void InitialiseItem(Item newItem, int count)
    {//wird beim Start verwendet und fügt alle Items ein
        item = newItem;
        item.draggableItem = this;
        image.sprite= newItem.Sprite;
        this.count = count;
        RefreshCount();
    }

    public void RefreshCount()
    {//zum aktualisieren der Zahl in der Ecke (wenn 1, dann ausblenden, wenn null, dann grau)
        counttext.text = count.ToString();
        bool textActive = count > 1;
        counttext.gameObject.SetActive(textActive);
        //Grau hinterlegen, falls man alle Moves dieser Art verwendet hat
        if (_inventoryManager.inventorySlots.Contains<InventorySlot>(gameObject.transform.parent.GetComponent<InventorySlot>())&&count == 0)
        {
            Image parantImage = transform.GetComponent<Image>();
            Color gray = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            parantImage.color = gray;
        }
        //Wieder normal machen, falls es zum ersten mal wieder 1 ist
        else if(_inventoryManager.inventorySlots.Contains<InventorySlot>(gameObject.transform.parent.GetComponent<InventorySlot>()) && count == 1)
        {
            Image parantImage = transform.GetComponent<Image>();
            Color gray = new Color(1f, 1f, 1f, 1f);
            parantImage.color = gray;
        }


    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_inventoryManager.inventorySlots.Contains<InventorySlot>(gameObject.transform.parent.GetComponent<InventorySlot>()) && count == 0)
        {//falls keines mehr da ist, dann nicht verschieben lassen
            enabled = false;
            image.raycastTarget = false;
            return;

        }
        if (_inventoryManager.inventorySlots.Contains<InventorySlot>(gameObject.transform.parent.GetComponent<InventorySlot>())&&count > 0)
        {
            // Erstelle eine echte Kopie des GameObjects
            countspeicher = count;
            count = 1;
            RefreshCount();
            startParent = transform.parent;
            parentAfterDrag = transform.parent;
            draggedItem = Instantiate(gameObject, transform.position, transform.rotation);
            draggedItem.transform.SetParent(transform.root);
            draggedItem.transform.SetAsLastSibling();

            // Erhalte die RectTransform-Komponente des Bilds der Kopie
            RectTransform draggedItemRectTransform = draggedItem.GetComponent<RectTransform>();
            // Setze die Größe der Kopie auf die gleiche Größe wie das Original
            RectTransform originalRectTransform = image.GetComponent<RectTransform>();
            draggedItemRectTransform.sizeDelta = originalRectTransform.sizeDelta;

            //von ursprungsort lösen und sichtbar machen
            draggedItemScript = draggedItem.GetComponent<DraggableItem>();
            draggedItemScript.startParent = transform.parent;
            draggedItemScript.parentAfterDrag = transform.parent;
            draggedItemScript.image.raycastTarget = false;
            draggedItemScript.transform.SetAsLastSibling();


            image.raycastTarget = false;
            //Count verringern und aktualisieren
            count = countspeicher - 1;
            RefreshCount();

        }
        else if(!_inventoryManager.inventorySlots.Contains<InventorySlot>(gameObject.transform.parent.GetComponent<InventorySlot>()) && count > 0)
        {
            //Es ist ein Move-Slot, dann sollen man es verschieben anstatt eine Kopie zu machen
            startParent = transform.parent;
            parentAfterDrag = transform.parent;
              transform.SetParent(transform.root);
               transform.SetAsLastSibling();
             image.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {//Während dem Verschieben
        if (draggedItem != null)
        {
            
            draggedItemScript.image.sprite = item.Sprite;
            

            draggedItemScript.transform.position = Input.mousePosition;
            
        }
        else
        {
            transform.position = Input.mousePosition;
        }
    }

    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedItem != null)//Es handelt sich also um eine Kopie
        {
            // Überprüfe, ob der Drop-Bereich ein InventorySlot ist
            InventorySlot dropSlot = eventData.pointerEnter ? eventData.pointerEnter.GetComponentInParent<InventorySlot>() : null;

            if (dropSlot != null)
            {
                //index finden
                Transform i = _inventoryManager.inventorySlots[_inventoryManager.GetItemIndex(draggedItemScript.item)].gameObject.transform;

                if(i == dropSlot.gameObject.transform)
                {
                    //Kopie zerstören und Count erneuern
                    Destroy(draggedItemScript.gameObject);
                    count++;
                    RefreshCount();
                }
                else
                {
                    if(parentAfterDrag != i)
                    {
                        // Die Kopie wurde über einem InventorySlot losgelassen
                        draggedItemScript.transform.SetParent(parentAfterDrag);
                    }
                    else
                    {
                        Destroy(draggedItemScript.gameObject);
                        // Erhöhe den Zähler des ursprünglichen Objekts
                        count++;
                        RefreshCount();
                    }
                }

                
            }
            else
            {
                // Die Kopie wurde über einem anderen Bereich losgelassen, zerstöre die Kopie
                Destroy(draggedItemScript.gameObject);
                // Erhöhe den Zähler des ursprünglichen Objekts
                count++;
                RefreshCount();
            }
        }
        else if(!_inventoryManager.inventorySlots.Contains<InventorySlot>(startParent.GetComponent<InventorySlot>()))
        {//Es handelt sich um Move Slot

            // Überprüfe, ob der Drop-Bereich ein InventorySlot ist
            InventorySlot dropSlot = eventData.pointerEnter ? eventData.pointerEnter.GetComponentInParent<InventorySlot>() : null;

            if (dropSlot != null)
            {
                // Das Originalobjekt wurde über einem InventorySlot losgelassen
                transform.SetParent(parentAfterDrag);
            }
            else
            {

                if (_inventoryManager.inventorySlots.Any(slot => slot.transform == startParent))
                {
                    Transform i = _inventoryManager.inventorySlots[_inventoryManager.GetItemIndex(item)].gameObject.transform;

                    if (i.childCount == 0)
                    {
                        transform.SetParent(i);
                    }
                    else
                    {
                        Destroy(gameObject);
                        Transform v = i.GetChild(0);
                        v.gameObject.transform.GetComponent<DraggableItem>().count++;
                        v.gameObject.transform.GetComponent<DraggableItem>().RefreshCount();
                    }
                    count++;
                    RefreshCount();
                    Destroy(gameObject);
                }
                else
                {
                    transform.SetParent(startParent);
                    image.raycastTarget = true;
                }


            }
        }

        image.raycastTarget = true;

        if (draggedItem != null)
        {
            draggedItemScript.parentAfterDrag = draggedItemScript.startParent;
            draggedItemScript.image.raycastTarget = true;
            draggedItem = null;
        }
    }

}
