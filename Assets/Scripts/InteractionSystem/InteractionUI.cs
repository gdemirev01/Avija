using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    private TextMeshProUGUI interactionAlert;
    private GameObject questGiverPanel;

    private UIController uiController;

    private TextMeshProUGUI questDescription;

    public GameObject questFieldPrefab;

    public Interactable.InteractionTypes typeOfAlert;

    void Start()
    {
        interactionAlert = GameObject.Find("InteractionAlert").GetComponent<TextMeshProUGUI>();
        questGiverPanel = GameObject.Find("QuestGiverPanel");
        uiController = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    public void ToggleAlert(bool state)
    {
        interactionAlert.text = System.Enum.GetName(typeof(Interactable.InteractionTypes), typeOfAlert);
        interactionAlert.enabled = state;
    }

    public void ToggleQuestGiverPanel(bool state)
    {
        uiController.TogglePanel(questGiverPanel, state);
    }

    private void LoadQuestDetails(Quest quest)
    {
        description.GetComponent<TextMeshProUGUI>().text = quest.text;
        //acceptButton.onClick.AddListener(() => questSystem.AddQuest(quest));
    }

    public void LoadQuestsList(List<Quest> quests)
    {

        foreach (Transform quest in this.transform.GetChild(1).GetChild(1))
        {
            GameObject.Destroy(quest.gameObject);
        }

        int index = 0;
        foreach (Quest quest in quests)
        {
            var button = Instantiate(questFieldPrefab, new Vector3(300, 300 + (100 * index), 0), Quaternion.identity);
            button.transform.SetParent(questGiverPanel.transform.GetChild(1));
            button.transform.GetChild(0).GetComponent<Text>().text = quest.name;

            var cpyIndex = index;
            button.GetComponent<Button>().onClick.AddListener(() => LoadQuestDetails(quests[cpyIndex]));
            index++;
        }
    }
}
