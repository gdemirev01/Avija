using System.Collections.Generic;

public class Blacksmith : Interactable
{
    private CharacterProps playerProps;
    private Inventory playerInventory;
    private QuestController questController;

    public List<Recipe> recipes;
    public Recipe chosenRecipe;

    private void Awake()
    {
        this.type = InteractionTypes.Blacksmith;
    }

    private void Start()
    {
        playerProps = PlayerManager.Instance.player.GetComponent<CharacterProps>();
        playerInventory = PlayerManager.Instance.inventory;
        questController = QuestController.Instance;
    }

    public override void Interact()
    {
        BlacksmithUI.Instance.SetBlacksmith(this);
        BlacksmithUI.Instance.UpdatePanel();
        BlacksmithUI.Instance.TogglePanel(true);

        questController.SendProgressForQuest("Blacksmith");
    }

    public void Craft()
    {
        if (playerProps.coins >= chosenRecipe.cost && playerInventory.ContainsAllItems(chosenRecipe.materials.ToArray()))
        {
            playerInventory.RemoveAllItems(chosenRecipe.materials.ToArray());

            playerProps.coins -= chosenRecipe.cost;

            for (int i = 0; i < chosenRecipe.resultItem.amount; i++)
            {
                PlayerManager.Instance.inventory.AddItem(chosenRecipe.resultItem.item);
            }
        }
        else if (playerProps.coins < chosenRecipe.cost)
        {
            UIController.Instance.SetAlertMessage("not enough money");
        }
        else if (!playerInventory.ContainsAllItems(chosenRecipe.materials.ToArray()))
        {
            UIController.Instance.SetAlertMessage("missing items");
        }
    }
}
