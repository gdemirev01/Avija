using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Items/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField]
    public Item item1;

    [SerializeField]
    public Item item2;

    [SerializeField]
    public Item resultItem;

    [SerializeField]
    public int cost;
}
