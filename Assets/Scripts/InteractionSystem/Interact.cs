using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private GameObject interactionObject;
    public InteractionUI interactionUI;
    private bool alertOpened = false;

    private void Update()
    {
        if(interactionObject && Input.GetKeyDown(KeyCode.E))
        {
            interactionObject.GetComponent<NPCInteractable>().Interact();
            interactionUI.ToggleAlert(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<NPCInteractable>())
        {
            interactionUI.typeOfAlert = "Fix this";
            interactionObject = other.gameObject;

            if (!alertOpened)
            {
                interactionUI.ToggleAlert(true);
                alertOpened = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactionObject = null;
        interactionUI.ToggleAlert(false);
        alertOpened = false;
    }
}
