using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : ItemSlot
{

    public override void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
