﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    public int coins;

    public List<Item> items;
    public CharacterProps playerProps;

    private void Awake()
    {
        this.type = InteractionTypes.Shop;
    }

    private void Start()
    {
        playerProps = PlayerManager.instance.player.GetComponent<CharacterProps>();
    }

    public void Buy(Item item)
    {
        if (playerProps.coins < item.cost)
        {
            //not enough coins message
            return;
        }

        coins += item.cost;
        playerProps.coins -= item.cost;

        Inventory.instance.Add(item);
        items.Remove(item);
    }

    public void Sell(Item item)
    {
        if (coins < item.cost)
        {
            //shopkeeper broke 
            return;
        }

        coins -= item.cost;
        playerProps.coins += item.cost;

        Inventory.instance.Remove(item);
        items.Add(item);
    }

    public override void Interact()
    {
        ShopUI.instance.SetShop(this);
        ShopUI.instance.ToggleShopPanel(true);
    }
}
