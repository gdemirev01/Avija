using UnityEngine;

public class QuestGiver : Interactable
{
    [HideInInspector]
    public Quest quest;

    private QuestController questController;
    private InteractionUI interactionUI;

    private void Awake()
    {
        this.type = InteractionTypes.Quest;
    }

    private void Start()
    {
        questController = QuestController.instance;
        interactionUI = InteractionUI.instance;

        if (quest.done)
        {
            LoadNextQuest();
        }
    }

    public Quest GetQuest()
    {
        return this.quest;
    }

    public void LoadQuest(Quest quest)
    {
        this.quest = quest;
    }

    public void RemoveQuest()
    {
        quest = null;
    }

    public override void Interact()
    {
        questController.SendProgressForQuest(GetComponent<CharacterProps>().name);
        if (quest == null)
        {
            type = InteractionTypes.Talk;
            interactionUI.Talk(GetComponent<CharacterProps>().lines);
        }
        else
        {
            interactionUI.LoadInfoInPanel(quest);
        }
    }

    public void LoadNextQuest()
    {
        if (!quest.done) { return; }

        this.quest = quest.GetNextQuest();
        if (this.quest.done)
        {
            LoadNextQuest();
            return;
        }
    }
}
