using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public enum InteractionTypes { Talk, Quest, Gather, Pick, Shop, Blacksmith };
    public InteractionTypes type;

    //public QuestController questController;

    //public InteractionUI interactionUI;

    public virtual void Interact()
    {
        //if(type == InteractionTypes.Quest)
        //{
        //    interactionUI.LoadQuestDetails(GetComponent<QuestGiver>().quest);
        //    interactionUI.ToggleQuestDetails(true);
        //}
        //else if(type == InteractionTypes.Talk)
        //{
        //    Debug.Log("Hello");
        //    Debug.Log(questController);
        //    questController.SendProgressForQuest(GetComponent<CharacterProps>().name);
        //}
    }
}
