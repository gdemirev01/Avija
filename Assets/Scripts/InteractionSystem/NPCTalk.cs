using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : Interactable
{
    private InteractionUI interactionUI;
    private QuestController questController;

    void Awake()
    {
        type = InteractionTypes.Talk;
    }

    private void Start()
    {
        interactionUI = InteractionUI.instance;
        questController = QuestController.instance;
    }

    public override void Interact()
    {
        var characterProps = GetComponent<CharacterProps>();

        base.Interact();
        interactionUI.Talk(characterProps.lines);
        questController.SendProgressForQuest(characterProps.name);
    }
}
