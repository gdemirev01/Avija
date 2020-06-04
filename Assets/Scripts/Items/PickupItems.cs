using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : Interactable
{
    public Item item;
    private QuestController questController;

    private Inventory inventory;

    private void Awake()
    {
        this.type = InteractionTypes.Pick;
    }

    private void Start()
    {
        inventory = PlayerManager.Instance.inventory;
        questController = QuestController.Instance;
    }

    public override void Interact()
    {
        inventory.AddItem(item);
        Destroy(gameObject);

        questController.SendProgressForQuest(item.name);
    }
}
