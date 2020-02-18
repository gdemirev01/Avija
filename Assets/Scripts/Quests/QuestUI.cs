using System.Linq;
using TMPro;
using UnityEngine;

public class QuestUI : Singleton<QuestUI>, IStaticPanel, IDynamicPanel
{
    private Quest loadedQuest;

    private UIController uiController;
    private QuestController questController;

    [SerializeField]
    private GameObject questPanel;
    [SerializeField]
    private GameObject questInfo;
    [SerializeField]
    private TextMeshProUGUI questDescriptionGUI;
    [SerializeField]
    private TextMeshProUGUI questProgress;
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private GameObject buttons;

    void Start()
    {
        uiController = UIController.Instance;
        questController = QuestController.Instance;

        questController.onQuestChangeCallback += UpdatePanel;
    }

    public void AbortQuest()
    {
        questController.AbortQuest(loadedQuest);
        ClearPanel();
    }

    public void TogglePanelWithButton()
    {
        uiController.TogglePanelAuto(questPanel);
        if(uiController.IsPanelOpen(questPanel))
        {
            questInfo.SetActive(false);
        }
    }

    public void ClearPanel()
    {
        uiController.LoadText(questDescriptionGUI, "");
        uiController.LoadText(questProgress, "");
        questInfo.SetActive(false);
    }

    public void TogglePanel(bool state)
    {
        uiController.TogglePanel(questPanel, state);
    }

    public void UpdatePanel()
    {
        TextSlot[] slots = uiController.CreateSlots<TextSlot>(buttons, buttonPrefab, questController.GetQuests().Count);

        uiController.LoadTextToSlots(slots, questController.GetQuests().Select(q => q.name).ToArray());

        for(int i = 0; i < questController.GetQuests().Count; i++)
        {
            var quest = questController.GetQuests()[i];
            slots[i].button.onClick.AddListener(() => LoadInfoInPanel(quest));
        }
    }

    public void LoadInfoInPanel(ScriptableObject info)
    {
        var quest = info as Quest;
        loadedQuest = quest;

        uiController.LoadText(questDescriptionGUI, quest.text);
        if (quest.done)
        {
            uiController.LoadText(questProgress, "completed");
        }
        else
        {
            uiController.LoadText(questProgress, quest.goal.GetCurrentChoice().ToString() + "\n\n" + quest.reward.ToString());
        }
        questInfo.SetActive(true);
    }
}
