using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{

    #region Singleton
    public static InteractionUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is another instance of interactionUI");
            return;
        }

        instance = this;
    }
    #endregion


    private UIController uiController;
    private QuestController questController;

    public TextMeshProUGUI interactionAlert;
    public Interactable.InteractionTypes typeOfAlert;

    public GameObject questDetails;
    public GameObject questDescription;
    public TextMeshProUGUI goals;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI exp;
    public Button acceptQuestButton;
    public Button cancelQuestButton;

    public TextMeshProUGUI GoalText;
    public GameObject questChoices;
    public GameObject choicesPanel;
    public GameObject choiceButtonPrefab;

    public GameObject talkingPanel;

    private void Start()
    {
        uiController = UIController.instance;
        questController = QuestController.instance;
    }

    public void ToggleAlert(bool state)
    {
        interactionAlert.text = System.Enum.GetName(typeof(Interactable.InteractionTypes), typeOfAlert);
        interactionAlert.enabled = state;
    }

    public void ToggleQuestDetails(bool state)
    {
        uiController.TogglePanel(questDetails, state);

        cancelQuestButton.onClick.AddListener(() => { ToggleQuestDetails(false); }) ;
    }
    
    public void ToggleQuestChoices(bool state)
    {
        uiController.TogglePanel(questChoices, state);
    }

    public void LoadQuest(QuestGiver giver, Quest quest)
    {
        if (!questController.QuestAlreadyActive(quest))
        {
            ToggleQuestDetails(true);
            LoadQuestDetails(giver, quest);
        }
        else
        {
            if(quest.goal.GetCurrentChoice().MustChoose())
            {
                LoadChoices(giver, quest);
                ToggleQuestChoices(true);
                ToggleQuestDetails(false);
            }
            else if(quest.goal.GetCurrentChoice().ReachedEndOfGoal())
            {
                quest.done = true;
                ToggleQuestDetails(true);
            }
            else
            {
                ToggleQuestDetails(true);
            }
            LoadQuestDetails(giver, quest);
        }
    }

    public void LoadQuestDetails(QuestGiver giver, Quest quest)
    {
        questDescription.GetComponent<TextMeshProUGUI>().text = quest.text;
        coins.text = quest.reward.coins.ToString();
        exp.text = quest.reward.exp.ToString();
        goals.text = quest.goal.GetCurrentChoice().ToString();

        acceptQuestButton.gameObject.SetActive(true);

        if (!questController.QuestAlreadyActive(quest))
        {
            acceptQuestButton.GetComponentInChildren<Text>().text = "Accept";
            acceptQuestButton.onClick.AddListener(() =>
            {
                questController.AddQuest(quest);
                ToggleQuestDetails(false);
            });
        }
        else if(quest.done)
        {
            acceptQuestButton.GetComponentInChildren<Text>().text = "Completed";
            acceptQuestButton.onClick.AddListener(() =>
            {
                questController.CompleteQuest(quest);
                ToggleQuestDetails(false);
            });
        }
        else
        {
            acceptQuestButton.gameObject.SetActive(false);
        }
    }

    public void LoadChoices(QuestGiver giver, Quest quest)
    {
        Goal goal = quest.goal;

        questChoices.GetComponentInChildren<TextMeshProUGUI>().text = goal.nextGoalText;

        foreach(Transform child in choicesPanel.transform)
        {
            Destroy(child.gameObject);
        }

        int index = 0;
        foreach (Goal option in goal.options)
        {
            var choice = Instantiate(choiceButtonPrefab, choicesPanel.transform, false).GetComponent<Button>();
            choice.transform.GetComponentInChildren<TextMeshProUGUI>().text = option.text;
            choice.transform.SetParent(choicesPanel.transform);

            var cpyIndex = index;
            choice.onClick.AddListener(() => {
                goal.ChooseOption(cpyIndex);
                LoadQuest(giver, quest);
                uiController.TogglePanel(questChoices, false);
            });
            index++;
        }
    }

    public void Talk(string lines)
    {
        talkingPanel.GetComponentInChildren<TextMeshProUGUI>().text = lines;
        uiController.TogglePanel(talkingPanel, true);
    }
}
