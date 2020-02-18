public class InventorySlot : ItemSlot
{
    private void Start()
    {
        button.onRight.AddListener(SellItem);
    }

    public void SellItem()
    {
        var shop = ShopUI.Instance.shop;
        if(shop != null)
        {
            shop.SellItemsToShop(this.item);
        }
    }
}
