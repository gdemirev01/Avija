using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : Interactable
{
    public Item item;
    public QuestController questController;

    private Inventory inventory;

    private void Awake()
    {
        this.type = InteractionTypes.Pick;
    }

    private void Start()
    {
        inventory = PlayerManager.instance.inventory;
    }

    public override void Interact()
    {
        inventory.AddItem(item);
        Destroy(gameObject);

        questController.SendProgressForQuest(item.name);
    }
}
