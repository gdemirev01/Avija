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
        questUI = GameObject.Find("QuestPanel").GetComponent<QuestUI>();
        LoadQuestsFromDirectory(Application.dataPath + "/Resources/Quests/");
        playerProps = GameObject.Find("Player").GetComponent<CharacterProps>();
        levelSystem = GameObject.Find("EventSystem").GetComponent<LevelSystem>();
    }

    public void SendProgressForKillQuest(string nameOfTarget)
    {
        foreach(Quest quest in activeQuests)
        {
            var part = quest.GetCurrentPart();
            if (quest.GetCurrentPartType().Equals("kill") && part["target"].Equals(nameOfTarget))
            {
                var progress = int.Parse(quest.GetCurrentPart()["progress"]);
                quest.GetCurrentPart()["progress"] = (progress + 1).ToString();

                if(part["progress"].Equals(part["quantity"]))
                {
                    quest.GetCurrentPart()["status"] = "completed";
                    quest.currentPart++;

                    if(quest.currentPart >= quest.partsNumber) {
                        quest.currentPart = quest.partsNumber;
                        Debug.Log("questCompleted");}
                } 
            }
        }
    }

    public void AddQuest(Quest quest)
    {
        if (activeQuests.Contains(quest)) { return; }
        activeQuests.Add(quest);
        questUI.UpdateQuestUI();
        Debug.Log("Quest " + quest.text + " accepted");
    }

    public void CompleteQuest(Quest quest)
    {
        levelSystem.AddExp(quest.exp);
        playerProps.coins += quest.coins;
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

    private void LoadQuestsFromDirectory(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] info = dir.GetFiles("*.json");
        info.Select(f => f.FullName).ToArray();
        foreach (FileInfo f in info)
        {
            var pathToFile = f.ToString();
            Quest quest = QuestParser.Deserialize(pathToFile);
            GameObject.Find(quest.giver).GetComponent<QuestGiver>().LoadQuest(quest);
        }
    }
}
