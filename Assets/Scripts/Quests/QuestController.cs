using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class QuestController : MonoBehaviour
{

    #region Singleton
    public static QuestController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is another instance of uiController");
            return;
        }

        instance = this;

        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();
    }
    #endregion

    private List<Quest> activeQuests;
    private List<Quest> completedQuests;

    private QuestUI questUI;
    private CharacterProps playerProps;
    private LevelController levelController;

    public GameObject NPCs;

    public delegate void OnQuestChange();
    public OnQuestChange onQuestChangeCallback;

    private void Start()
    {
        questUI = QuestUI.instance;
        playerProps = PlayerManager.instance.player.GetComponent<CharacterProps>();
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
        if (nextQuest.giver == null) { return; }

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
        if (activeQuests.Contains(quest)) { return; }

        activeQuests.Add(quest);

        onQuestChangeCallback.Invoke();
    }

    public void CompleteQuest(Quest quest)
    {
        levelController.AddExp(quest.reward.exp);
        playerProps.coins += quest.reward.coins;

        if (quest.reward.item != null)
        {
            PlayerManager.instance.inventory.AddItem(quest.reward.item);
        }

        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        var nextQuest = quest.GetNextQuest();
        if(nextQuest.done)
        {
            CompleteQuest(nextQuest);
            return;
        }

        LoadToGiver(nextQuest);

        onQuestChangeCallback.Invoke();
    }

    public void AbortQuest(Quest quest)
    {
        activeQuests.Remove(quest);

        quest.Reset();

        onQuestChangeCallback.Invoke();
    }
}
