using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{ 
    private UIController uiController;
    private QuestController questController;

    private TextMeshProUGUI interactionAlert;
    public Interactable.InteractionTypes typeOfAlert;

    private GameObject questDetails;
    private GameObject questDescription;
    private Button acceptQuestButton;


    void Start()
    {
        uiController = GameObject.Find("Canvas").GetComponent<UIController>();
        questController = GameObject.Find("EventSystem").GetComponent<QuestController>();

        interactionAlert = GameObject.Find("InteractionAlert").GetComponent<TextMeshProUGUI>();

        questDetails = GameObject.Find("QuestDetails");
        questDescription = questDetails.transform.GetChild(0).gameObject;
        acceptQuestButton = questDetails.transform.GetComponentInChildren<Button>();
    }

    public void ToggleAlert(bool state)
    {
        interactionAlert.text = System.Enum.GetName(typeof(Interactable.InteractionTypes), typeOfAlert);
        interactionAlert.enabled = state;
    }

    public void ToggleQuestDetails(bool state)
    {
        uiController.TogglePanel(questDetails, state);
    }

    public void LoadQuestDetails(Quest quest)
    {
        questDescription.GetComponent<TextMeshProUGUI>().text = quest.text;

        acceptQuestButton.onClick.AddListener(() => {
            questController.AddQuest(quest);
            uiController.TogglePanel(questDetails, false);
        });
    }
}
