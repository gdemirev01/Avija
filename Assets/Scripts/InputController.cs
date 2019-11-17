using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : MonoBehaviour
{

    private ComboSystem comboSystem;
    private PlayerMovement playerMovement;
    private Animator animator;
    private UIController uiController;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        comboSystem = GameObject.Find("PlayerWeapon").GetComponent<ComboSystem>();
        animator = GetComponent<Animator>();
        uiController = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && GetComponent<PlayerMovement>().inBattle)
        {
            GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().Attack();
            comboSystem.RestartTimer();
        }

        if (Input.GetButtonDown("DrawSword"))
        {
            playerMovement.ChangeStance();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            uiController.ToggleCanvas(false);
        }
    }

    public void ToggleWeapon(string parameters)
    {
        string[] p = parameters.Split(' ');
        var state = p[1] == "true";
        GameObject.Find(p[0]).GetComponent<MeshRenderer>().enabled = state;
    }
}
