using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : Interactable
{
    public enum InteractionTypes { Talk, Quest };
    public InteractionTypes type;

    public QuestController questController;

    public InteractionUI interactionUI;

    public override void Interact()
    {
        if(type == InteractionTypes.Quest)
        {
            var quest = GetComponent<QuestGiver>().quest;
            if (quest == null)
            {
                type = InteractionTypes.Talk;
            }
            else
            {
                interactionUI.LoadQuest(this, quest);
            }
        }
        else if(type == InteractionTypes.Talk)
        {
            questController.SendProgressForQuest(GetComponent<CharacterProps>().name);
        }
    }
}
