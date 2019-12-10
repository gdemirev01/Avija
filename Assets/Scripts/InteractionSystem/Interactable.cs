using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public enum InteractionTypes { Talk, Quest, Gather, Pick };
    public InteractionTypes type;

    private QuestController questController;

    private InteractionUI interactionUI;

    private void Start()
    {
        interactionUI = GameObject.Find("InteractionPanel").GetComponent<InteractionUI>();
        questController = GameObject.Find("EventSystem").GetComponent<QuestController>();
    }

    public void Interact()
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
