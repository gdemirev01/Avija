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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("NPC"))
        {
            interactionObject = other.gameObject;
        }
    }
}
