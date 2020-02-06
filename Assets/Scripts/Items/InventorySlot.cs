using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public TextMeshProUGUI amount;
    public ClickableObject button;

    Item item;

    private void Start()
    {
        button.onLeft.AddListener(UseItem);
        button.onRight.AddListener(SellItem);
    }

    public void AddItem(ItemAmount newItem)
    {
        item = newItem.item;

        amount.text = newItem.amount.ToString();

        this.icon.sprite = item.icon;
        this.icon.enabled = true;
    }

    public void ClearSlot()
    {
        if(item == null) { return; }
        
        item = null;

        amount.text = "";

        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }

    public void SellItem()
    {
        var shop = ShopUI.instance.shop;
        if(shop != null)
        {
            shop.Sell(this.item);
        }
    }
}
