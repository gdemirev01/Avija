using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemAmount
{
    [SerializeField]
    public Item item;

    [SerializeField]
    public int amount;

    public ItemAmount(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}
