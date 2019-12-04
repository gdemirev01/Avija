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
    public GameObject buttonPrefab;

    void Start()
    {
        quests = new List<Quest>();

        uiController = this.transform.root.GetComponent<UIController>();
        questController = GameObject.Find("EventSystem").GetComponent<QuestController>();

        questDescriptionGUI = GameObject.Find("QuestDescription").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateQuestUI()
    {
        quests = questController.GetQuests();
    }

    public void ToggleQuestPanel()
    {
        UpdateQuestUI();
        UpdatePanel();
        panelOpened = !panelOpened;
        uiController.TogglePanel(this.gameObject, panelOpened);
    }

    public void UpdatePanel()
    {
        Debug.Log(quests.Count);

        if(this.transform.childCount >= 2) { 
            foreach(Transform quest in this.transform.GetChild(1))
            {
                GameObject.Destroy(quest.gameObject);
            }
        }

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
        uiController.LoadText(questDescriptionGUI, quests[index].text);
    }
}
