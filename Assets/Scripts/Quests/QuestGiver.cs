using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine;
using System.Collections.Generic;


public class QuestGiver : MonoBehaviour
{

    public Quest quest;

    public void LoadQuest(Quest quest)
    {
        this.quest = quest;
    }

    public void RemoveQuest()
    {
        quest = null;
    }
}
