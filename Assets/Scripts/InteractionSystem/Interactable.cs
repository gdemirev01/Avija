using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public enum InteractionTypes { Talk, Quest, Gather, Pick };
    public InteractionTypes type;

    private InteractionUI interactionUI;

    private void Start()
    {
        interactionUI = GameObject.Find("InteractionPanel").GetComponent<InteractionUI>();
    }

    public void Interact()
    {
        if(type == InteractionTypes.Quest)
        {
            interactionUI.LoadQuestDetails(GetComponent<QuestGiver>().quest);
            interactionUI.ToggleQuestDetails(true);
        }
    }
}
