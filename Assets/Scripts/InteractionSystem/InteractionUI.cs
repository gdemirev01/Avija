using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour, IStaticPanel
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
    private ChoiceUI choiceUI;
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

    public GameObject talkingPanel;

    private void Start()
    {
        uiController = UIController.instance;
        choiceUI = ChoiceUI.instance;
        questController = QuestController.instance;
        
        cancelQuestButton.onClick.AddListener(() => { TogglePanel(false); });
    }

    public void ToggleAlert(bool state)
    {
        interactionAlert.text = System.Enum.GetName(typeof(Interactable.InteractionTypes), typeOfAlert);
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
        throw new System.NotImplementedException();
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
