using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOfMoveSlots : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> _inventorySlots;
    [HideInInspector] public List<Item> moveOptions;

    public Button StartButton;

    public List<Item> CollectMoveOptions()
    {
        moveOptions.Clear();
        foreach(InventorySlot inventory in _inventorySlots)
        {
            if(inventory.transform.childCount != 0)
            {
                moveOptions.Add(inventory.SelectedMove);
            }
        }
        return moveOptions;
    }
}
