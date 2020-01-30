using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public enum InteractionTypes { Talk, Quest, Pick, Shop, Blacksmith };
    public InteractionTypes type;


    public virtual void Interact() {}

}
