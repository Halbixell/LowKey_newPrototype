
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
    {
        item = newItem;
        item.draggableItem = this;
        image.sprite= newItem.Sprite;
        this.count = count;
        RefreshCount();
    }

    public void RefreshCount()
    {
        counttext.text = count.ToString();
        bool textActive = count > 1;
        counttext.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        

        if(count==1)
        {
            startParent = transform.parent;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;

            
        }
        if (count > 1)
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


            draggedItemScript = draggedItem.GetComponent<DraggableItem>();
            draggedItemScript.startParent = transform.parent;
            draggedItemScript.parentAfterDrag = transform.parent;
            draggedItemScript.image.raycastTarget = false;
            draggedItemScript.transform.SetAsLastSibling();




            image.raycastTarget = false;
            count = countspeicher - 1;
            RefreshCount();

        }
        //else
        //{
        //    startParent = transform.parent;
        //    parentAfterDrag = transform.parent;
        //    transform.SetParent(transform.root);
        //    transform.SetAsLastSibling();
        //    image.raycastTarget = false;
        //}
        


        



    }

    public void OnDrag(PointerEventData eventData)
    {
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
        if (draggedItem != null)
        {
            
            // Überprüfe, ob der Drop-Bereich ein InventorySlot ist
            InventorySlot dropSlot = eventData.pointerEnter ? eventData.pointerEnter.GetComponentInParent<InventorySlot>() : null;

            if (dropSlot != null)
            {
                Debug.Log("Dies ist eine Kopie");
                Transform i = _inventoryManager.inventorySlots[_inventoryManager.GetItemIndex(draggedItemScript.item)].gameObject.transform;

                if(i == dropSlot.gameObject.transform)
                {
                    Debug.Log("JA, du hast recht.");
                    Destroy(draggedItemScript.gameObject);
                    // Erhöhe den Zähler des ursprünglichen Objekts
                    count++;
                    RefreshCount();
                }
                else
                {
                    Debug.Log("Nein, du hast nicht recht");
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
                Debug.Log("Nicht über slot");
                // Die Kopie wurde über einem anderen Bereich losgelassen, zerstöre die Kopie
                Destroy(draggedItemScript.gameObject);

                // Erhöhe den Zähler des ursprünglichen Objekts
                count++;
                RefreshCount();
            }
        }
        else
        {
            
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
                    Debug.Log("Der Startknoten war im Inventoryslot.");
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

        if (count == 0)
        {
            Destroy(gameObject);
        }

        if (draggedItem != null)
        {
            draggedItemScript.parentAfterDrag = draggedItemScript.startParent;
            draggedItemScript.image.raycastTarget = true;
            draggedItem = null;
        }
    }

}
