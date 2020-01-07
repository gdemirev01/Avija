﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private List<Quest> quests;
    private List<Quest> activeQuests;

    public CharacterProps playerProps;
    private LevelSystem levelSystem;

    public QuestUI questUI;

    private void Awake()
    {
        quests = new List<Quest>();
        activeQuests = new List<Quest>();
    }

    private void Start()
    {
        levelSystem = this.GetComponent<LevelSystem>();
    }


    public void SendProgressForQuest(string nameOfTarget)
    {
        foreach(Quest quest in activeQuests)
        {
            var goal = quest.goal.GetCurrentChoice();
            if (goal.target.Equals(nameOfTarget))
            {
                goal.progress++;

                if (goal.progress >= goal.quantity)
                {
                    goal.progress = goal.quantity;
                    goal.done = true;
                }
            }
        }
    }

    public void AddQuest(Quest quest)
    {
        if (activeQuests.Contains(quest)) { return; }

        activeQuests.Add(quest);
        questUI.UpdateQuestUI();
    }

    public void CompleteQuest(Quest quest)
    {
        levelSystem.AddExp(quest.reward.exp);
        playerProps.coins += quest.reward.coins;

        activeQuests.Remove(quest);
        questUI.UpdateQuestUI();
    }

    public void AbortQuest(Quest quest)
    {
        activeQuests.Remove(quest);
        questUI.UpdateQuestUI();
    }

    public bool QuestAlreadyActive(Quest quest)
    {
        return activeQuests.Contains(quest);
    }

    public List<Quest> GetQuests()
    {
        return activeQuests;
    }
}
