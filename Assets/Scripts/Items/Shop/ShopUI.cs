using UnityEngine;

public class ShopUI : Singleton<ShopUI>, IDynamicPanel
{
    private UIController uiController;

    [HideInInspector]
    public Shop shop;

    [SerializeField]
    private GameObject itemsParent;
    
    [SerializeField]
    private GameObject ShopPanel;
   
    [SerializeField]
    private GameObject shopItemPrefab;


    private void Start()
    {
        uiController = UIController.Instance;
    }

    private void Update()
    {
        if (!UIController.Instance.IsPanelOpen(ShopPanel))
        {
            shop = null;
        }
    }

    public void SetShop(Shop shop)
    {
        this.shop = shop;
        UpdatePanel();
    }

    public void ClearPanel()
    {
        uiController.ClearChildrenOfPanel(itemsParent);
    }

    public void UpdatePanel()
    {
        var slots = uiController.CreateSlots<ShopSlot>(itemsParent, shopItemPrefab, shop.GetItemsCount());
        uiController.LoadItemsToSlots(slots, shop.GetItems());
        foreach(ShopSlot slot in slots)
        {
            slot.SetShop(this.shop);
        }
    }

    public void TogglePanel(bool state)
    {
        UIController.Instance.TogglePanel(ShopPanel, state);
    }
}
