using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Item;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    //public Dictionary<Item, int> maxStacks = new Dictionary<Item, int>();
    public Item[] itemsToPickup;
    [SerializeField] private List<int> MaxStackList;

    private Item currentAutoFillItem;
    private int currentAutoFillCount;



    public GameObject inventoryItemPrefab;
    [HideInInspector] public bool alredyhere = false;
    //public int maxStack = 8;

    [HideInInspector]public static InventoryManager Instance { get; private set; }

    void Awake()
    {
        // Stelle sicher, dass es nur eine Instanz des InventoryManagers gibt
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    void Start()
    {
        AutoFillInventory();
    }

    
    void AutoFillInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            if (slot != null)
            {
                if (i < itemsToPickup.Length)
                {
                    currentAutoFillItem = itemsToPickup[i];
                    currentAutoFillCount = MaxStackList[i];

                    // Check if the item is already in the inventory
                    DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
                    if (itemInSlot != null && itemInSlot.item == currentAutoFillItem)
                    {
                        alredyhere = true;
                        currentAutoFillCount -= itemInSlot.count;
                    }

                    // Add the remaining count of the item to the slot
                    if (currentAutoFillCount > 0)
                    {
                        if (alredyhere == false)
                        {
                            SpawnNewItem(slot);
                            alredyhere = true;
                        }
                    }
                }
            }
            alredyhere = false;
        }
    }



    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

            if (itemInSlot != null && itemInSlot.item == item)
            {
                alredyhere = true;
            }

            if (itemInSlot != null && itemInSlot.item == item)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // Wenn das Item nicht im Inventar ist und kein DraggableItem existiert
        DraggableItem draggableItem = item.draggableItem;

        if (draggableItem == null)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

                if (itemInSlot == null)
                {
                    draggableItem = GetDraggableItem(item); // Änderung hier
                    slot.AddItemToSlot(draggableItem);
                    return true;
                }
            }
        }

        alredyhere = false;
        return false;
    }
    public DraggableItem GetDraggableItem(Item item)
    {
        for (int i = 0; i < itemsToPickup.Length; i++)
        {
            if (itemsToPickup[i] == item)
            {
                

                
                GameObject newDraggableItemGO = new GameObject("DraggableItem");

               
                DraggableItem draggableItem = newDraggableItemGO.AddComponent<DraggableItem>();

                
                draggableItem.InitialiseItem(item, 1); // Du kannst die Anfangszählung anpassen

                return draggableItem;
            }
        }

        return null;
    }

    public int GetItemIndex(Item item)
    {
        for (int i = 0; i < itemsToPickup.Length; i++)
        {
            if (itemsToPickup[i] == item)
            {
                return i;
            }
        }
        return -1; // Rückgabewert für den Fall, dass das Item nicht gefunden wird
    }


    void SpawnNewItem(InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        DraggableItem inventoryItem = newItemGo.GetComponent<DraggableItem>();
        inventoryItem.InitialiseItem(currentAutoFillItem, currentAutoFillCount);
    }



    public void AddItemToSlot(Item item)
    {
        int itemIndex = GetItemIndex(item);

        if (itemIndex != -1)
        {
            // Überprüfen, ob das Item in den InventorySlots vorhanden ist
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

                if (itemInSlot != null && itemInSlot.item == item)
                {
                    // Füge das Item dem Slot hinzu
                    DraggableItem draggableItem = GetDraggableItem(item);
                    slot.AddItemToSlot(draggableItem);
                    return;
                }
            }

            // Wenn das Item nicht in den InventorySlots ist, füge es dem entsprechenden Slot hinzu
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

                if (itemInSlot == null)
                {
                    SpawnNewItem(slot, item);
                    return;
                }
            }
        }
    }

    void SpawnNewItem(InventorySlot slot, Item item)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        DraggableItem inventoryItem = newItemGo.GetComponent<DraggableItem>();
        inventoryItem.InitialiseItem(item, 1); // Du kannst die Anfangszählung anpassen
    }



}
