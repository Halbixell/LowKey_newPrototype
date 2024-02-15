using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEntry
{
    public Item move;
    public int direction;

    public ItemEntry(Item move, int direction)
    {
        this.move = move;
        this.direction = direction;
    }
}

public class ListOfMoveSlots : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> _inventorySlots;
    public List<Item> moveOptions;
    public List<ItemEntry> MovesAndRotations = new List<ItemEntry>();

    public Button StartButton;

    public List<ItemEntry> CollectMoveOptionsandRotation()
    {
        MovesAndRotations.Clear();
        foreach (InventorySlot inventory in _inventorySlots)
        {
            if (inventory.transform.childCount != 0)
            {
                ItemEntry temp = new ItemEntry(inventory.SelectedMove, inventory.direction);
                MovesAndRotations.Add(temp);
                

                Debug.Log("<color=red>Added Move </color>" + inventory.SelectedMove.Name + " <color=red>in direction </color>" + inventory.direction);
                inventory.direction = 0;
            }
        }
        return MovesAndRotations;
    }


}
