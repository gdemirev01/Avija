using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private List<Quest> quests;
    private List<Quest> activeQuests;

    void Start()
    {
        Debug.Log(Application.dataPath);
        LoadQuestsFromDirectory(Application.dataPath + "/Resources/Quests/");
        quests = new List<Quest>();
        activeQuests = new List<Quest>();
    }

    public void AddQuest(Quest quest)
    {
        if (activeQuests.Contains(quest)) { return; }
        activeQuests.Add(quest);
        Debug.Log("Quest " + quest.text + " accepted");
    }

    public void AbortQuest(Quest quest)
    {
        activeQuests.Remove(quest);
    }

    public List<Quest> GetQuests()
    {
        return quests;
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
            GameObject.Find(quest.giver).GetComponent<QuestGiver>().AddQuest(quest);
        }
    }
}
