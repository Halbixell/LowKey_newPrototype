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

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        
        if (draggableItem != null)
        {
            
            AddItemToSlot(draggableItem);
            SelectedMove = draggableItem.item;
        }
    }




    public void AddItemToSlot(DraggableItem draggableItem)
    {
        if (!_inventoryManager.inventorySlots.Contains<InventorySlot>(this))
        {
            if (transform.childCount == 0)
            {
                draggableItem.parentAfterDrag = transform;
            }
            else if (transform.childCount == 1)
            {
                Transform currentChild = transform.GetChild(0);
                GameObject storedItem = currentChild.gameObject;

                // Setze das aktuelle Objekt in den Slot des gezogenen Objekts
                draggableItem.parentAfterDrag = transform;

                DraggableItem StoredDraggableItem = currentChild.GetComponent<DraggableItem>();

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
                // Setze das gezogene Objekt an die Position des vorherigen Objekts im Slot
                //storedItem.transform.SetParent(draggableItem.startParent);


                // Aktualisiere die Position des vorherigen Objekts im Slot
                storedItem.transform.SetAsLastSibling();
            }
        }
    }



}