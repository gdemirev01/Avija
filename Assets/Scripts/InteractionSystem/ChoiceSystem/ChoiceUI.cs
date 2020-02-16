using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour, IStaticPanel
{
    #region Singleton
    public static ChoiceUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is another instance of shopkeeperUI");
            return;
        }

        instance = this;
    }
    #endregion

    private UIController uiController;
    private InteractionUI interactionUI;

    public TextMeshProUGUI GoalText;
    public GameObject choicePanel;
    public GameObject choicesPanel;
    public GameObject choiceButtonPrefab;

    private void Start()
    {
        uiController = UIController.instance;
        interactionUI = InteractionUI.instance;
    }

    public void ClearPanel()
    {
        throw new System.NotImplementedException();
    }

    public void TogglePanel(bool state)
    {
        uiController.TogglePanel(choicePanel, state);
    }

    public void UpdatePanel()
    {
        throw new System.NotImplementedException();
    }

    public void LoadInfoInPanel(ScriptableObject info)
    {
        var quest = info as Quest;

        Goal goal = quest.goal;

        choicePanel.GetComponentInChildren<TextMeshProUGUI>().text = goal.nextGoalText;

        foreach (Transform child in choicesPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Goal option in goal.options)
        {
            var choice = Instantiate(choiceButtonPrefab, choicesPanel.transform, false).GetComponent<Button>();
            choice.transform.GetComponentInChildren<TextMeshProUGUI>().text = option.text;
            choice.transform.SetParent(choicesPanel.transform);

            choice.onClick.AddListener(() => {
                goal.ChooseOption(option);
                interactionUI.LoadInfoInPanel(quest);
                uiController.TogglePanel(choicePanel, false);
            });
        }
    }
}
