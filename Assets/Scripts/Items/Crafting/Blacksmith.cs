using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : Interactable
{

    private CharacterProps playerProps;

    private void Awake()
    {
        this.type = InteractionTypes.Blacksmith;
    }

    private void Start()
    {
        playerProps = PlayerManager.instance.player.GetComponent<CharacterProps>();
    }

    public override void Interact()
    {
        //Collect the two items and the recipe from the ui
        // Craft the item
    }

    public void Craft(CraftingRecipe recipe, Item item1, Item item2)
    {
        if(recipe.item1.Equals(item1) &&
            recipe.item2.Equals(item2))
        {

            if(playerProps.coins >= recipe.cost)
            {
                playerProps.coins -= recipe.cost;
             
                //Remove the two items

                Inventory.instance.Add(recipe.resultItem);
            }
            else
            {
                //ui alert not enough money
                Debug.Log("not enough money");
            }
        }
    }
}
