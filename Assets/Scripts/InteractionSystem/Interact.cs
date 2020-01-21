using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Transform source;
    public InteractionUI interactionUI;
    private PlayerMovement playerMovement;

    new private Rigidbody rigidbody;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody = transform.root.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(source.position, source.TransformDirection(Vector3.forward), out hit, 7))
        {
            Debug.DrawRay(source.position, source.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
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
            Debug.DrawRay(source.position, source.TransformDirection(Vector3.forward) * 1000, Color.red);
            interactionUI.ToggleAlert(false);
        }
    } 
}
