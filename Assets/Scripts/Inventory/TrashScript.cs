using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class TrashScript : MonoBehaviour, IDropHandler
{
    public InventoryManager inventoryManager;
    public DraggableItem draggableItemScript;

    public void OnDrop(PointerEventData eventData)
    {
        int itemIndex = 0;
        if (eventData.pointerDrag != null)
        {
            DraggableItem draggedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

            if (draggedItem != null)
            {
                if (inventoryManager.moveSlots.Contains<InventorySlot>(draggedItem.startParent.GetComponent<InventorySlot>()))
                {
                    RectTransform trashCanRect = GetComponent<RectTransform>();
                    Vector2 localMousePosition;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(trashCanRect, Input.mousePosition, eventData.pressEventCamera, out localMousePosition);
                    
                    if (trashCanRect.rect.Contains(localMousePosition))
                    {
                        itemIndex = inventoryManager.GetItemIndex(draggedItem.item);

                        if (itemIndex != -1)
                        {
                            InventorySlot slot = inventoryManager.inventorySlots[itemIndex];
                            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

                            slot.AddItemToSlot(draggedItem);
                            itemInSlot.count++;
                            itemInSlot.RefreshCount();
                            Destroy(draggedItem.gameObject);
                            

                        }
                    }
                }
               
            }
        }
    }
}
