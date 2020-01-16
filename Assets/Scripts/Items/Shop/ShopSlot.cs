using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Image icon;

    private Shop shop;

    Item item;

    public void SetShop(Shop shop)
    {
        this.shop = shop;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = newItem.icon;
    }

    public void BuyItem()
    {
        if (item != null)
        {
            shop.Buy(item);
        }
    }

    public void SellItem()
    {
        if (item != null)
        {
            shop.Sell(item);
        }
    }
}
