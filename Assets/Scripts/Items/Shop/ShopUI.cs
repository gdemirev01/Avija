using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
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


    public GameObject itemsParent;

    InventorySlot[] slots;

    private Shop shop;

    public GameObject shopItemPrefab;

    void Start()
    {
    }

    public void SetShop(Shop shop)
    {
        this.shop = shop;
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < shop.items.Count; i++)
        {
            var shopItem = Instantiate(shopItemPrefab, itemsParent.transform, false);
            var slotScript = shopItem.GetComponent<ShopSlot>();
            slotScript.AddItem(shop.items[i]);
            slotScript.SetShop(this.shop);
        }
    }

    public void ToggleShopPanel(bool state)
    {
        UIController.instance.TogglePanel(this.gameObject, state);
    }
}
