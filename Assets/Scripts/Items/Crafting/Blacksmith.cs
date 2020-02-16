using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Blacksmith : Interactable
{

    private CharacterProps playerProps;
    private Inventory playerInventory;

    public List<Recipe> recipes;
    public Recipe choosenRecipe;

    private void Awake()
    {
        this.type = InteractionTypes.Blacksmith;
    }

    private void Start()
    {
        playerProps = PlayerManager.instance.player.GetComponent<CharacterProps>();
        playerInventory = PlayerManager.instance.inventory;
    }

    public override void Interact()
    {
        BlacksmithUI.instance.SetBlacksmith(this);
        BlacksmithUI.instance.UpdatePanel();
        BlacksmithUI.instance.TogglePanel(true);
    }

    public void Craft()
    {
        if (playerInventory.ContainsAllItems(choosenRecipe.materials.ToArray()))
        {
            playerInventory.RemoveAllItems(choosenRecipe.materials.ToArray());

            if (playerProps.coins >= choosenRecipe.cost)
            {
                playerProps.coins -= choosenRecipe.cost;

                for(int i = 0; i < choosenRecipe.resultItem.amount; i++)
                    PlayerManager.instance.inventory.AddItem(choosenRecipe.resultItem.item);
            }
            else
            {
                UIController.instance.SetAlertMessage("not enough money");
            }
        }
        else
        {
            UIController.instance.SetAlertMessage("missing items");

        }
    }
}
