﻿using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine;
using System.Collections.Generic;


public class QuestGiver : Interactable
{

    public Quest quest;
    public int asdf = 0;
    public QuestController questController;
    public InteractionUI interactionUI;

    private void Awake()
    {
        this.type = InteractionTypes.Quest;
    }

    public void LoadQuest(Quest quest)
    {
        this.quest = quest;
    }

    public void RemoveQuest()
    {
        quest = null;
    }

    public override void Interact()
    {
        questController.SendProgressForQuest(GetComponent<CharacterProps>().name);
        if (quest == null)
        {
            type = InteractionTypes.Talk;
            interactionUI.Talk(GetComponent<CharacterProps>());
        }
        else
        {
            interactionUI.LoadQuest(this, quest);
        }
    }
}
