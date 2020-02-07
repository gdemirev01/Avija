﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    private List<Quest> quests;
    private bool panelOpened = false;

    private UIController uiController;
    public QuestController questController;

    public TextMeshProUGUI questDescriptionGUI;
    public TextMeshProUGUI questProgress;
    public GameObject buttonPrefab;

    public Quest loadedQuest;

    void Start()
    {
        quests = new List<Quest>();

        uiController = this.transform.root.GetComponent<UIController>();
    }

    public void UpdateQuestUI()
    {
        quests = questController.GetQuests();
        UpdatePanel();
    }

    private void UpdatePanel()
    {
        if(this.transform.childCount >= 2) { 
            foreach(Transform quest in this.transform.GetChild(2))
            {
                GameObject.Destroy(quest.gameObject);
            }
        }

        int index = 0;
        foreach(Quest quest in quests)
        {
            var button = Instantiate(buttonPrefab, this.transform.GetChild(2).transform, false);
            button.transform.SetParent(this.transform.GetChild(2));
            button.transform.GetChild(0).GetComponent<Text>().text = quest.name;

            var cpyIndex = index;
            button.GetComponent<Button>().onClick.AddListener(() => LoadQuestInfoInPanel(cpyIndex));
            index++;
        }
    }

    public void ToggleQuestPanel()
    {
        quests = questController.GetQuests();
        if (quests.Count > 0) { LoadQuestInfoInPanel(0); }

        panelOpened = !panelOpened;
        uiController.TogglePanel(this.gameObject, panelOpened);
    }

    public void AbortQuest()
    {
        questController.AbortQuest(loadedQuest);
        ClearPanel();
    }


    public void LoadQuestInfoInPanel(int index)
    {
        var quest = quests[index];
        loadedQuest = quest;
        uiController.LoadText(questDescriptionGUI, quest.text);

        if (quest.done)
        {
            uiController.LoadText(questProgress, "completed");
        }
        else
        {
            uiController.LoadText(questProgress, quest.goal.GetCurrentChoice().ToString() + "\n\n" + quest.reward.ToString());
        }
    }

    public void ClearPanel()
    {
        uiController.LoadText(questDescriptionGUI, "");
        uiController.LoadText(questProgress, "");
    }
}
