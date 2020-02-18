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
                TogglePanel(false);
            });
        }
        else if(quest.done)
        {
            acceptQuestButton.GetComponentInChildren<Text>().text = "Completed";
            acceptQuestButton.onClick.AddListener(() =>
            {
                questController.CompleteQuest(quest);
                TogglePanel(false);
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
            TogglePanel(true);
            LoadQuestDetails(quest);
        }
        else
        {
            if (quest.goal.GetCurrentChoice().MustChoose())
            {
                choiceUI.LoadInfoInPanel(quest);
                choiceUI.TogglePanel(true);
                TogglePanel(false);
            }
            else if (quest.goal.GetCurrentChoice().ReachedEndOfGoal())
            {
                quest.done = true;
                TogglePanel(true);
            }
            else
            {
                TogglePanel(true);
            }
            LoadQuestDetails(quest);
        }
    }
}
