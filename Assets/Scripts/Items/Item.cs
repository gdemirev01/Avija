using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public int cost;

    public Sprite icon;

    public virtual void Use() { }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}

