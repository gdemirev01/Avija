using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private GameObject interactionObject;
    private InteractionUI interactionUI;
    private bool alertOpened = false;

    private void Start()
    {
        interactionUI = GameObject.Find("InteractionPanel").GetComponent<InteractionUI>();
    }

    private void Update()
    {
        if(interactionObject && Input.GetKeyDown(KeyCode.E))
        {
            interactionObject.GetComponent<Interactable>().Interact();
            interactionUI.ToggleAlert(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Interactable>())
        {
            interactionUI.typeOfAlert = other.GetComponent<Interactable>().type;
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
