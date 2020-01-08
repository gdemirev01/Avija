using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : Interactable
{
    public int coins;

    public List<Item> items;
    public CharacterProps playerProps;

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
}
