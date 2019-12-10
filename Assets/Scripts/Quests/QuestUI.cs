using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    private List<Quest> quests;
    private bool panelOpened = false;

    private UIController uiController;
    private QuestController questController;

    private TextMeshProUGUI questDescriptionGUI;
    private TextMeshProUGUI questProgress;
    public GameObject buttonPrefab;

    void Start()
    {
        quests = new List<Quest>();

        uiController = this.transform.root.GetComponent<UIController>();
        questController = GameObject.Find("EventSystem").GetComponent<QuestController>();

        questDescriptionGUI = GameObject.Find("QuestDescription").GetComponent<TextMeshProUGUI>();
        questProgress = GameObject.Find("QuestProgress").GetComponent<TextMeshProUGUI>();
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
            var button = Instantiate(buttonPrefab, new Vector3(300, 600 + (-100 * index), 0), Quaternion.identity);
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

    public void LoadQuestInfoInPanel(int index)
    {
        Debug.Log(index);
        var quest = quests[index];
        uiController.LoadText(questDescriptionGUI, quest.text);

        if (quest.GetCurrentGoal().done)
        {
            uiController.LoadText(questProgress, "completed");
        }
        else
        {
            
            uiController.LoadText(questProgress, quest.GetCurrentGoal().ToString());
        }
    }
}
