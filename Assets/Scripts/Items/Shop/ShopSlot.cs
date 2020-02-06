using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{

    private Shop shop;

    Item item;

    public Image icon;

    public int quantity;

    public TextMeshProUGUI cost;

    public void SetShop(Shop shop)
    {
        this.shop = shop;
    }

    public void AddItem(ItemAmount newItem)
    {
        item = newItem.item;

        icon.sprite = newItem.item.icon;

        this.quantity = newItem.amount;

        this.cost.text = newItem.item.cost.ToString();
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
