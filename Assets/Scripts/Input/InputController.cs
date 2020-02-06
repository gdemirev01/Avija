using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : MonoBehaviour
{

    private PlayerMovement playerMovement;
    public Animator animator;
    public UIController uiController;
    public DealDamage dealDamage;

    private bool togglePointer = false;

    void Start()
    {
        playerMovement = transform.GetChild(0).GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && playerMovement.inBattle)
        {
            dealDamage.Attack();
            GetComponent<Timer>().RestartTimer();
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

        if(Input.GetButtonDown("ToggleMouse"))
        {
            togglePointer = !togglePointer;
            TogglePointer(togglePointer);
        }

        if(Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("jump");
        }

        if(Input.GetButtonDown("Block"))
        {
            playerMovement.Block(true);
        }
        if (Input.GetButtonUp("Block"))
        {
            playerMovement.Block(false);
        }

        if(Input.GetButtonDown("Cast"))
        {
            animator.SetTrigger("castSpell");
        }
    }

    public void TogglePointer(bool state)
    {
        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        playerMovement.canMove = !state;
    }
}
