using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>, IDynamicPanel
{
    private Inventory inventory;
    private UIController uiController;
    private CharacterProps characterProps;

    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private GameObject itemsParent;

    [SerializeField]
    private GameObject slotPrefab;
    
    private List<InventorySlot> slots;

    [SerializeField]
    private TextMeshProUGUI coins;

    public override void Awake()
    {
        base.Awake();

        slots = new List<InventorySlot>();
    }

    void Start()
    {
        inventory = PlayerManager.Instance.inventory;
        characterProps = PlayerManager.Instance.player.GetComponent<CharacterProps>();
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
        coins.text = characterProps.coins.ToString();
    }

    public void TogglePanelWithButton()
    {
        uiController.TogglePanelAuto(inventoryPanel);
    }

    public void TogglePanel(bool state)
    {
        uiController.TogglePanel(inventoryPanel, state);
    }

    public void ClearPanel()
    {
        foreach(InventorySlot slot in slots)
        {
            Destroy(slot.gameObject);
        }
    }
}
