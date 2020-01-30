using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : Interactable
{
    public InteractionUI interactionUI;

    void Awake()
    {
        type = InteractionTypes.Talk;
    }


    public override void Interact()
    {
        base.Interact();
        interactionUI.Talk(GetComponent<CharacterProps>());
    }
}
