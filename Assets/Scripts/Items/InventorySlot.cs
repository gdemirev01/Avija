public class InventorySlot : ItemSlot
{
    private void Start()
    {
        button.onRight.AddListener(SellItem);
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
