using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionUI : Singleton<InteractionUI>, IStaticPanel
{
    private UIController uiController;
    private ChoiceUI choiceUI;
    private QuestController questController;

    [SerializeField]
    private TextMeshProUGUI interactionAlert;
    
    public InteractionTypes typeOfAlert;

    [SerializeField]
    private GameObject questDetails;
    [SerializeField]
    private GameObject questDescription;
    [SerializeField]
    private TextMeshProUGUI goals;
    [SerializeField]
    private TextMeshProUGUI coins;
    [SerializeField]
    private TextMeshProUGUI exp;
    [SerializeField]
    private Button acceptQuestButton;
    [SerializeField]
    private Button cancelQuestButton;

    public GameObject talkingPanel;

    private void Start()
    {
        choiceUI = ChoiceUI.Instance;
        uiController = UIController.Instance;
        questController = QuestController.Instance;
        
        cancelQuestButton.onClick.AddListener(() => { TogglePanel(false); });
    }

    public void ToggleAlert(bool state)
    {
        interactionAlert.text = System.Enum.GetName(typeof(InteractionTypes), typeOfAlert);
        interactionAlert.enabled = state;
    }

    public void LoadQuestDetails(Quest quest)
    {
        questDescription.GetComponent<TextMeshProUGUI>().text = quest.text;
        coins.text = "Coins " + quest.reward.coins.ToString();
        exp.text = "EXP " + quest.reward.exp.ToString();
        goals.text = quest.goal.GetCurrentChoice().ToString();

        string state = questController.QuestAlreadyActive(quest) ? "Completed" : "Accept";

        acceptQuestButton.gameObject.SetActive(true);
        acceptQuestButton.GetComponentInChildren<Text>().text = state;
        acceptQuestButton.onClick.AddListener(() =>
        {
            TogglePanel(false);
        });

        if (!questController.QuestAlreadyActive(quest))
        {
            acceptQuestButton.onClick.AddListener(() =>
            {
                questController.AddQuest(quest);
            });
        }
        else if(quest.done)
        {
            acceptQuestButton.onClick.AddListener(() =>
            {
                questController.CompleteQuest(quest);
            });
        }
        else
        {
            acceptQuestButton.gameObject.SetActive(false);
        }
    }

    public void Talk(string lines)
    {
        talkingPanel.GetComponentInChildren<TextMeshProUGUI>().text = lines;
        uiController.TogglePanel(talkingPanel, true);
    }

    public void TogglePanel(bool state)
    {
        uiController.TogglePanel(questDetails, state);
    }

    public void ClearPanel()
    {
        questDescription.GetComponent<TextMeshProUGUI>().text = "";
        coins.text = "";
        exp.text = "";
        goals.text = "";
    }

    public void LoadInfoInPanel(ScriptableObject info)
    {
        var quest = info as Quest;
        
        if (!questController.QuestAlreadyActive(quest))
        {
            LoadQuestDetails(quest);
        }
        else
        {
            if (quest.goal.GetCurrentChoice().MustChoose())
            {
                choiceUI.LoadInfoInPanel(quest);
                choiceUI.TogglePanel(true);
                TogglePanel(false);
                return;
            }
            else if (quest.goal.GetCurrentChoice().ReachedEndOfGoal())
            {
                quest.done = true;
            }
            LoadQuestDetails(quest);
        }
    }
}
