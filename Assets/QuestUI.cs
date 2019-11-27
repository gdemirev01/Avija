﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    private List<Quest> quests;
    private UIController uiController;
    private TextMeshProUGUI questDescriptionGUI;
    public GameObject buttonPrefab;
    private bool panelOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        uiController = this.transform.root.GetComponent<UIController>();
        quests = GetComponent<QuestSystem>().GetQuests();
        questDescriptionGUI = GameObject.Find("QuestDescription").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateQuestUI()
    {
        quests = GetComponent<QuestSystem>().GetQuests();
    }

    public void ToggleQuestPanel()
    {
        UpdatePanel();
        uiController.TogglePanel(this.gameObject, !panelOpened);
    }

    public void UpdatePanel()
    {

        int index = 0;
        foreach(Quest quest in quests)
        {
            var button = Instantiate(buttonPrefab, new Vector3(300, 600 + (-100 * index), 0), Quaternion.identity);
            button.transform.SetParent(this.transform);
            button.transform.GetChild(0).GetComponent<Text>().text = quest.name;
            var cpyIndex = index;
            button.GetComponent<Button>().onClick.AddListener(() => LoadQuestInfoInPanel(cpyIndex));
            index++;
        }
    }

    public void LoadQuestInfoInPanel(int index)
    {
        Debug.Log(index);
        uiController.LoadText(questDescriptionGUI, quests[index].questText);
    }
}
