using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashScript : MonoBehaviour, IDropHandler
{
    public InventoryManager inventoryManager; // Verweise auf deinen InventoryManager

    public void OnDrop(PointerEventData eventData)
    {
        int itemIndex = 0;
        if (eventData.pointerDrag != null)
        {
            DraggableItem draggedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

            if (draggedItem != null)
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
                        // Überprüfen, ob das Item in den InventorySlots vorhanden ist
                        
                        if (itemInSlot != null)
                        {
                            Debug.Log("Es befindet sich das gleiche Item noch im Inventory ");
                            // Füge das Item dem Slot hinzu
                            slot.AddItemToSlot(draggedItem);
                            itemInSlot.count++;
                            itemInSlot.RefreshCount();
                            Destroy(draggedItem.gameObject);

                        }
                        else
                        {
                            Debug.Log("Das Item befindet sich nicht mehr im Inventory");
                            // Wenn das Item nicht in den InventorySlots ist, füge es dem entsprechenden Slot hinzu
                            inventoryManager.AddItemToSlot(draggedItem.item);
                            Destroy(draggedItem.gameObject);
                        }
                        

                        
                    }
                }
            }
        }
    }
}
