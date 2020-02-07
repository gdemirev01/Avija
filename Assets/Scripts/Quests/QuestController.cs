using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private List<Quest> activeQuests;
    public List<Quest> completedQuests;

    private CharacterProps playerProps;
    private LevelSystem levelSystem;

    public QuestUI questUI;

    public GameObject NPCs;

    private void Awake()
    {
        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();
    }

    private void Start()
    {
        playerProps = PlayerManager.instance.player.GetComponent<CharacterProps>();
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

    public void LoadToGiver(Quest nextQuest)
    {
        if (nextQuest.giver == null) { return; }

        foreach (Transform npc in NPCs.transform)
        {
            CharacterProps props = npc.gameObject.GetComponent<CharacterProps>();

            if (props.name.Equals(nextQuest.giver))
            {
                npc.gameObject.GetComponent<QuestGiver>().quest = nextQuest;
                return;
            }
        }
    }

    public void CompleteQuest(Quest quest)
    {
        levelSystem.AddExp(quest.reward.exp);
        playerProps.coins += quest.reward.coins;

        PlayerManager.instance.inventory.AddItem(quest.reward.item);

        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        LoadToGiver(quest.GetNextQuest());

        questUI.UpdateQuestUI();
    }

    public void AbortQuest(Quest quest)
    {
        activeQuests.Remove(quest);

        quest.Reset();

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
