using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private GameObject storedItem;
    [SerializeField] private InventoryManager _inventoryManager;
    public Item SelectedMove;
    public int SlotIndex = -1;
    [HideInInspector] public int direction;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        
        if (draggableItem != null)
        {
            
            AddItemToSlot(draggableItem);
            draggableItem.item.ChangeDirection(direction%4);

            SelectedMove = draggableItem.item;
            direction = 0;

        }
    }




    public void AddItemToSlot(DraggableItem draggableItem)
    {
        

       if (!_inventoryManager.inventorySlots.Contains<InventorySlot>(this))
            //D.h: Gibt es einen slot, in dem dieses Ding was ich hier habe, drin ist
        {
            if (transform.childCount == 0)
            {
                
                draggableItem.parentAfterDrag = transform;
            }
            else if (transform.childCount == 1)
            {
                Transform currentChild = transform.GetChild(0);
                GameObject storedItem = currentChild.gameObject;
                draggableItem.parentAfterDrag = transform;

                DraggableItem StoredDraggableItem = currentChild.GetComponent<DraggableItem>();
                //index suchen
                Transform i = _inventoryManager.inventorySlots[_inventoryManager.GetItemIndex(StoredDraggableItem.item)].gameObject.transform;

                if (i.childCount == 0)
                {
                    storedItem.transform.SetParent(i);
                    
                }
                else
                {
                    
                    Destroy(StoredDraggableItem.gameObject);
                    Transform v = i.GetChild(0);
                    v.gameObject.transform.GetComponent<DraggableItem>().count++;
                    v.gameObject.transform.GetComponent<DraggableItem>().RefreshCount();
                }
                // Aktualisiere die Position des vorherigen Objekts im Slot
                storedItem.transform.SetAsLastSibling();
            }
        }
        
    }

}