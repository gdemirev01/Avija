using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : Interactable
{
    public Item item;
    public QuestController questController;

    private void Awake()
    {
        this.type = InteractionTypes.Pick;
    }

    public override void Interact()
    {
        Inventory.instance.Add(item);
        Destroy(gameObject);

        questController.SendProgressForQuest(item.name);
    }
}
