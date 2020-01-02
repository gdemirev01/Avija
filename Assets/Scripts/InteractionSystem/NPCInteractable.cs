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
            interactionUI.LoadQuestDetails(GetComponent<QuestGiver>().quest);
            interactionUI.ToggleQuestDetails(true);
        }
        else if(type == InteractionTypes.Talk)
        {
            Debug.Log("Hello");
            Debug.Log(questController);
            questController.SendProgressForQuest(GetComponent<CharacterProps>().name);
        }
    }
}
