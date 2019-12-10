using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{ 
    public UIController uiController;
    public QuestController questController;

    public TextMeshProUGUI interactionAlert;
    public Interactable.InteractionTypes typeOfAlert;

    public GameObject questDetails;
    public GameObject questDescription;
    public Button acceptQuestButton;
    public TextMeshProUGUI goals;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI exp;

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
        coins.text = quest.reward.coins.ToString();
        exp.text = quest.reward.exp.ToString();
        goals.text = quest.GetGoalsList();

        if (quest.GetCurrentGoal().done)
        {
            acceptQuestButton.GetComponentInChildren<Text>().text = "Complete";
            acceptQuestButton.onClick.AddListener(() =>
            {
                questController.CompleteQuest(quest);
                uiController.TogglePanel(questDetails, false);
            });
        }
        else
        {
            acceptQuestButton.onClick.AddListener(() =>
            {
                questController.AddQuest(quest);
                uiController.TogglePanel(questDetails, false);
            });
        }
    }
}
