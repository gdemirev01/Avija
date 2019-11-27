using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private GameObject interactionObject;

    private void Update()
    {
        if(interactionObject && Input.GetKeyDown(KeyCode.E))
        {
            interactionObject.GetComponent<NPCControler>().Interact();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("NPC"))
        {
            interactionObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactionObject = null;
    }
}
