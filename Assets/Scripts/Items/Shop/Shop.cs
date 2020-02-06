using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    public int coins;

    [SerializeField]
    public Inventory inventory;

    private CharacterProps playerProps;

    private void Awake()
    {
        this.type = InteractionTypes.Shop;
    }

    private void Start()
    {
        playerProps = PlayerManager.instance.player.GetComponent<CharacterProps>();
        inventory.onItemChangedCallback += ShopUI.instance.UpdateUI;
    }

    private void RemoveItemFromShop(Item item)
    {
        inventory.RemoveItem(item);
    }

    public void Buy(Item item)
    {
        if (playerProps.coins < item.cost)
        {
            UIController.instance.SetAlertMessage("Not enough coins");
            return;
        }

        coins += item.cost;
        playerProps.coins -= item.cost;

        PlayerManager.instance.inventory.AddItem(item);

        RemoveItemFromShop(item);
    }

    public void Sell(Item item)
    {
        if (coins < item.cost)
        {
            UIController.instance.SetAlertMessage("Shopkeeper can't afford that");
            return;
        }

        coins -= item.cost;
        playerProps.coins += item.cost;

        PlayerManager.instance.inventory.RemoveItem(item);
        inventory.AddItem(item);
    }

    public Item GetItem(int index)
    {
        return inventory.GetItem(index);
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
        ShopUI.instance.SetShop(this);
        ShopUI.instance.ToggleShopPanel(true);
    }
}
