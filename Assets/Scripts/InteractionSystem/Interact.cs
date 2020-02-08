using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Transform sourceOfRaycast;

    private InteractionUI interactionUI;

    private void Start()
    {
        interactionUI = InteractionUI.instance;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(sourceOfRaycast.position, sourceOfRaycast.TransformDirection(Vector3.forward), out hit, 7))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if(interactable != null)
            {
                interactionUI.typeOfAlert = interactable.type;
                interactionUI.ToggleAlert(true);
                if (Input.GetButtonDown("Interact"))
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            interactionUI.ToggleAlert(false);
        }
    } 
}
