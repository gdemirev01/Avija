using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public InteractionTypes type;

    public abstract void Interact();
}
