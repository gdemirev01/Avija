using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    private List<Quest> quests;
    // Start is called before the first frame update
    void Start()
    {
        quests = new List<Quest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddQuest(Quest quest)
    {
        if(quests.Contains(quest)) { return; }
        quests.Add(quest);
        Debug.Log("Quest " + quest.questText + " accepted");
    }

    public void AbortQuest(Quest quest)
    {
        quests.Remove(quest);
    }

    public List<Quest> GetQuests()
    {
        return quests;
    }
}
