using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : Interactable
{

    public Item item;

    public override void Interact()
    {
        Inventory.instance.Add(item);
        Destroy(gameObject);
    }
}
