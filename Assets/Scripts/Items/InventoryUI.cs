using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    private Inventory inventory;

    public GameObject itemsParent;

    InventorySlot[] slots;

    private bool inventoryOpened = false;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateUI()
    {
        for(int i = 0; i < inventory.space; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void ToggleInventoryPanel()
    {
        inventoryOpened = !inventoryOpened;
        UIController.instance.TogglePanel(itemsParent, inventoryOpened);
    }
}
