using UnityEngine;

public class ShopUI : MonoBehaviour, IDynamicPanel
{
    #region Singleton
    public static ShopUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is another instance of shopkeeperUI");
            return;
        }

        instance = this;
    }
    #endregion

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
        uiController = UIController.instance;
    }

    private void Update()
    {
        if (!UIController.instance.isPanelOpened(ShopPanel))
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
        throw new System.NotImplementedException();
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
        UIController.instance.TogglePanel(ShopPanel, state);
    }
}
