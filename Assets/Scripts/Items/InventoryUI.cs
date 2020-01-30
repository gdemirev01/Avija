using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    private Inventory inventory;

    public GameObject itemsParent;

    InventorySlot[] slots;

    [SerializeField]
    GameObject slotPrefab;

    private bool inventoryOpened = false;

    void Start()
    {
        inventory = Inventory.instance;

        inventory.onItemChangedCallback += UpdateUI;

        InitializeSlots();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();


        // update for the loaded items from save file
        UpdateUI();
    }

    void InitializeSlots()
    {
        for(int i = 0; i < inventory.space; i++)
        {
            GameObject slot = Instantiate(slotPrefab, itemsParent.transform, false);
        }
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
