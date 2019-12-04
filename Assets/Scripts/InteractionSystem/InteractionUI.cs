using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{ 
    private UIController uiController;
    private QuestController questController;

    public GameObject questFieldPrefab;

    private TextMeshProUGUI interactionAlert;
    public Interactable.InteractionTypes typeOfAlert;

    private GameObject questGiverPanel;

    private GameObject questsList;

    private GameObject questDetails;
    private GameObject questDescription;
    private Button acceptQuestButton;


    void Start()
    {
        uiController = GameObject.Find("Canvas").GetComponent<UIController>();
        questController = GameObject.Find("EventSystem").GetComponent<QuestController>();

        interactionAlert = GameObject.Find("InteractionAlert").GetComponent<TextMeshProUGUI>();

        questGiverPanel = GameObject.Find("QuestGiverPanel");

        questsList = GameObject.Find("QuestsList");

        questDetails = GameObject.Find("QuestDetails");
        questDescription = questDetails.transform.GetChild(0).gameObject;
        acceptQuestButton = GameObject.Find("QuestDetails").transform.GetComponentInChildren<Button>();

    }

    public void ToggleAlert(bool state)
    {
        interactionAlert.text = System.Enum.GetName(typeof(Interactable.InteractionTypes), typeOfAlert);
        interactionAlert.enabled = state;
    }

    public void ToggleQuestGiverPanel(bool state)
    {
        uiController.TogglePanel(questGiverPanel, state);
        uiController.TogglePanel(questsList, state);
    }

    private void LoadQuestDetails(Quest quest)
    {
        uiController.TogglePanel(questsList, false);
        uiController.TogglePanel(questDetails, true);

        questDescription.GetComponent<TextMeshProUGUI>().text = quest.text;
        Debug.Log(questDescription.GetComponent<TextMeshProUGUI>().text);
        acceptQuestButton.onClick.AddListener(() => {
            questController.AddQuest(quest);
            uiController.TogglePanel(questDetails, false);
            uiController.TogglePanel(questsList, true);
        });
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
            button.GetComponent<Button>().onClick.AddListener(() => LoadQuestDetails(quest));
            index++;
        }
    }
}
