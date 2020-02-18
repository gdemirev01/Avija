using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : Singleton<ChoiceUI>, IStaticPanel
{
    private UIController uiController;
    private InteractionUI interactionUI;

    [SerializeField]
    private GameObject choicePanel;
    [SerializeField]
    private GameObject choicesPanel;
    [SerializeField]
    private GameObject choiceButtonPrefab;

    private void Start()
    {
        uiController = UIController.Instance;
        interactionUI = InteractionUI.Instance;
    }

    public void TogglePanel(bool state)
    {
        uiController.TogglePanel(choicePanel, state);
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

    public void ClearPanel()
    {
        uiController.ClearChildrenOfPanel(choicesPanel);
    }
}
