using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public InteractionUI interactionUI;
    private bool alertOpened = false;

    public Transform source;

    public PlayerMovement playerMovement;

    private void Update()
    {

        RaycastHit hit;
        if (Physics.Raycast(source.position, source.TransformDirection(Vector3.forward), out hit, 7))
        {
            Debug.DrawRay(source.position, source.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if(interactable != null)
            {
                interactionUI.ToggleAlert(true);
                if (Input.GetButtonDown("Interact"))
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            Debug.DrawRay(source.position, source.TransformDirection(Vector3.forward) * 1000, Color.red);
            interactionUI.ToggleAlert(false);
        }

        if (Physics.Raycast(source.position, source.TransformDirection(Vector3.down), out hit, 3))
        {
            if (hit.collider.gameObject.transform.tag.Equals("Water"))
            {
                playerMovement.inWater = true;
            }
        }

    } 
}
