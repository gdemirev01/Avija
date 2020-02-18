using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    public int coins;

    [SerializeField]
    private Inventory inventory;

    private CharacterProps playerProps;

    private void Awake()
    {
        this.type = InteractionTypes.Shop;
    }

    private void Start()
    {
        playerProps = PlayerManager.Instance.player.GetComponent<CharacterProps>();
        inventory.onItemChangedCallback += ShopUI.Instance.UpdatePanel;
    }

    private void RemoveItemFromShop(Item item)
    {
        inventory.RemoveItem(item);
    }

    public void BuyItemsFromShop(Item item)
    {
        if (playerProps.coins < item.cost)
        {
            UIController.Instance.SetAlertMessage("Not enough coins");
            return;
        }

        coins += item.cost;
        playerProps.coins -= item.cost;

        PlayerManager.Instance.inventory.AddItem(item);

        RemoveItemFromShop(item);
    }

    public void SellItemsToShop(Item item)
    {
        if (coins < item.cost)
        {
            UIController.Instance.SetAlertMessage("Shopkeeper can't afford that");
            return;
        }

        coins -= item.cost;
        playerProps.coins += item.cost;

        PlayerManager.Instance.inventory.RemoveItem(item);
        inventory.AddItem(item);
    }

    public Item GetItem(int index)
    {
        return inventory.GetItem(index);
    }

    public ItemAmount[] GetItems()
    {
        return inventory.items.ToArray();
    }

    public ItemAmount GetItemAmount(int index)
    {
        return inventory.GetItemAmount(index);
    }

    public int GetItemsCount()
    {
        return inventory.ItemsCount();
    }

    public override void Interact()
    {
        ShopUI.Instance.SetShop(this);
        ShopUI.Instance.TogglePanel(true);
    }
}
