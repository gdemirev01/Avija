using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControler : MonoBehaviour
{
    public float range = 5;
    public bool playerInRange = false;
    private GameObject Player;
    private bool panelOpened = false;
    private UIController canvasController;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        canvasController = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        canvasController.ToggleCanvas(true);
        canvasController.LoadText("Hello Adventurer");
    }
}
