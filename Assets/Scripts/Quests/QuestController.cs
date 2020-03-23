using System.Collections.Generic;
using UnityEngine;

public class QuestController : Singleton<QuestController>
{
    private List<Quest> activeQuests;
    private List<Quest> completedQuests;

    private QuestUI questUI;
    private CharacterProps playerProps;
    private LevelController levelController;

    public GameObject NPCs;

    public delegate void OnQuestChange();
    public OnQuestChange onQuestChangeCallback;

    public override void Awake()
    {
        base.Awake();

        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();
    }

    private void Start()
    {
        questUI = QuestUI.Instance;
        playerProps = PlayerManager.Instance.player.GetComponent<CharacterProps>();
        levelController = this.GetComponent<LevelController>();
    }

    public List<Quest> GetQuests()
    {
        return activeQuests;
    }

    public bool QuestAlreadyActive(Quest quest)
    {
        return activeQuests.Contains(quest);
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

    public void LoadToGiver(Quest nextQuest)
    {
        if (nextQuest.giver == null)
        {
            return;
        }

        foreach (Transform npc in NPCs.transform)
        {
            CharacterProps props = npc.gameObject.GetComponent<CharacterProps>();

            if (props.name.Equals(nextQuest.giver))
            {
                npc.gameObject.GetComponent<QuestGiver>().LoadQuest(nextQuest);
                return;
            }
        }
    }
    
    public void AddQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        { 
            return;
        }

        activeQuests.Add(quest);

        onQuestChangeCallback.Invoke();
    }

    public void AddQuests(Quest[] quests)
    {
        for(int i = 0; i < quests.Length; i++)
        {
            AddQuest(quests[i]);
        }
    }

    public void CompleteQuest(Quest quest)
    {
        levelController.AddExp(quest.reward.exp);
        playerProps.coins += quest.reward.coins;

        if (quest.reward.item != null)
        {
            PlayerManager.Instance.inventory.AddItem(quest.reward.item);
        }

        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        var nextQuest = quest.GetNextQuest();
        if (nextQuest != null)
        {
            if (nextQuest.done)
            {
                CompleteQuest(nextQuest);
                return;
            }
            LoadToGiver(nextQuest);
        }

        questUI.ClearPanel();

        onQuestChangeCallback.Invoke();
    }

    public void AbortQuest(Quest quest)
    {
        activeQuests.Remove(quest);

        quest.Reset();

        onQuestChangeCallback.Invoke();
    }

    public void ResetAllQuests()
    {
        Quest[] quests = Resources.LoadAll<Quest>("Quests");
        
        foreach(Quest quest in quests)
        {
            quest.Reset();
        }
    }
}
