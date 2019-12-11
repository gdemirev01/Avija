﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : MonoBehaviour
{

    public ComboSystem comboSystem;
    private PlayerMovement playerMovement;
    private Animator animator;
    public UIController uiController;
    private bool togglePointer = false;
    public DealDamage dealDamage;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && GetComponent<PlayerMovement>().inBattle)
        {
            dealDamage.Attack();
            comboSystem.RestartTimer();
        }

        if (Input.GetButtonDown("DrawSword"))
        {
            playerMovement.ChangeStance();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            var lastPanel = uiController.GetLastPanel();
            if (lastPanel != null)
            {
                uiController.TogglePanel(lastPanel, false);
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            togglePointer = !togglePointer;
            TogglePointer(togglePointer);
        }
    }

    public void TogglePointer(bool state)
    {
        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ToggleWeapon(string parameters)
    {
        string[] p = parameters.Split(' ');
        var state = p[1] == "true";
        GameObject.Find(p[0]).GetComponent<MeshRenderer>().enabled = state;
    }
}
