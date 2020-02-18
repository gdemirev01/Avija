using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>, IDynamicPanel
{

    private Inventory inventory;
    private UIController uiController;
    
    [SerializeField]
    private GameObject itemsParent;

    [SerializeField]
    private GameObject slotPrefab;
    
    private List<InventorySlot> slots;

    public override void Awake()
    {
        base.Awake();

        slots = new List<InventorySlot>();
    }

    void Start()
    {
        inventory = PlayerManager.Instance.inventory;
        uiController = UIController.Instance;

        inventory.onItemChangedCallback += UpdatePanel;

        InitializeSlots();

        UpdatePanel();
    }

    void InitializeSlots()
    {
        for(int i = 0; i < inventory.space; i++)
        {
            slots.Add(Instantiate(slotPrefab, itemsParent.transform, false).GetComponent<InventorySlot>());
        }
    }

    public void UpdatePanel()
    {
        for (int i = 0; i < inventory.space; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void TogglePanelWithButton()
    {
        uiController.TogglePanelAuto(itemsParent);
    }

    public void TogglePanel(bool state)
    {
        uiController.TogglePanel(itemsParent, state);
    }

    public void ClearPanel()
    {
        foreach(InventorySlot slot in slots)
        {
            Destroy(slot.gameObject);
        }
    }
}
