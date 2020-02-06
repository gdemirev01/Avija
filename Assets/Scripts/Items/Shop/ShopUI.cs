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

    public Shop shop;

    public GameObject shopItemPrefab;

    public void SetShop(Shop shop)
    {
        this.shop = shop;
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach(Transform child in itemsParent.transform) {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < shop.GetItemsCount(); i++)
        {
            var shopItem = Instantiate(shopItemPrefab, itemsParent.transform, false);
            var slotScript = shopItem.GetComponent<ShopSlot>();
            slotScript.AddItem(shop.GetItemAmount(i));
            slotScript.SetShop(this.shop);
        }
    }

    public void ToggleShopPanel(bool state)
    {
        UIController.instance.TogglePanel(this.gameObject, state);
    }
}
