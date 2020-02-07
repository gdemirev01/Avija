using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Items/Crafting Recipe")]
public class Recipe : Item
{
    [SerializeField]
    public List<ItemAmount> materials;

    [SerializeField]
    public ItemAmount resultItem;

    public override void Use()
    {
        base.Use();
        BlacksmithUI.instance.LoadRecipe(this);
    }
}
