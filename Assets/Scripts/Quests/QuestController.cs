using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private List<Quest> quests;
    private List<Quest> activeQuests;

    private CharacterProps playerProps;
    private LevelSystem levelSystem;

    private QuestUI questUI;

    private void Awake()
    {
        quests = new List<Quest>();
        activeQuests = new List<Quest>();
    }

    void Start()
    {
        playerProps = GameObject.Find("Player").GetComponent<CharacterProps>();
        levelSystem = GameObject.Find("EventSystem").GetComponent<LevelSystem>();
        questUI = GameObject.Find("QuestPanel").GetComponent<QuestUI>();
    }

    public void SendProgressForQuest(string nameOfTarget)
    {
        foreach(Quest quest in activeQuests)
        {
            var goal = quest.GetCurrentGoal();
            if (goal.target.Equals(nameOfTarget) && !goal.done)
            {
                goal.progress++;

                if (goal.progress == goal.quantity)
                {
                    goal.done = true;
                    quest.currentGoal++;

                    if (quest.currentGoal >= quest.goals.Count)
                    {
                        quest.currentGoal = quest.goals.Count - 1;
                    }
                }
            }
        }
    }

    public void AddQuest(Quest quest)
    {
        if (activeQuests.Contains(quest)) { return; }

        quest.GetCurrentGoal().done = false;

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

    public List<Quest> GetQuests()
    {
        return activeQuests;
    }
}
