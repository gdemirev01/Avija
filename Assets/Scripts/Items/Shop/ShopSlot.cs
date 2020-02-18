using TMPro;

public class ShopSlot : ItemSlot
{
    private Shop shop;

    public TextMeshProUGUI cost;

    public override void AddItem(ItemAmount item)
    {
        base.AddItem(item);

        this.cost.text = item.item.cost.ToString();
    }

    public override void UseItem()
    {
        BuyItem();
    }

    public void SetShop(Shop shop)
    {
        this.shop = shop;
    }

    public void BuyItem()
    {
        if (item != null)
        {
            shop.BuyItemsFromShop(item);
        }
    }

    public void SellItem()
    {
        if (item != null)
        {
            shop.SellItemsToShop(item);
        }
    }
}
