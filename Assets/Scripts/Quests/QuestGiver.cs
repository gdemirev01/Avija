using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine;
using System.Collections.Generic;


public class QuestGiver : MonoBehaviour
{

    public Quest quest;
    private InteractionUI interactionUI;

    private void Awake()
    {
        interactionUI = GameObject.Find("InteractionPanel").GetComponent<InteractionUI>();
    }

    public void LoadQuest(Quest quest)
    {
        this.quest = quest;
        interactionUI.LoadQuestDetails(quest);
    }
}
