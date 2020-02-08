using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private CombatController combatController;
    private UIController uiController;

    public Animator animator;
    public DealDamage dealDamage;

    private bool togglePointer = false;

    void Start()
    {
        playerMovement = transform.GetChild(0).GetComponent<PlayerMovement>();
        uiController = UIController.instance;
        combatController = CombatController.instance;
    }

    void Update()
    {
        UIInput();
        CombatInput();
        MovementInput();
    }

    private void MovementInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("jump");
        }
    }

    private void CombatInput()
    {
        if (Input.GetButtonDown("Attack"))
        {
            combatController.Attack();
        }

        if (Input.GetButtonDown("DrawSword"))
        {
            combatController.ToggleCombatMode();
        }

        if(Input.GetButtonDown("Block"))
        {
            combatController.Block(true);
        }
        else if (Input.GetButtonUp("Block"))
        {
            combatController.Block(false);
        }

        if (Input.GetButtonDown("Cast"))
        {
            animator.SetTrigger("castSpell");
        }
    }

    private void UIInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var lastPanel = uiController.GetLastPanel();
            if (lastPanel != null)
            {
                uiController.TogglePanel(lastPanel, false);
            }
        }

        if (Input.GetButtonDown("ToggleMouse"))
        {
            togglePointer = !togglePointer;
            TogglePointer(togglePointer);
        }
    }

    public void TogglePointer(bool state)
    {
        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        playerMovement.canMove = !state;
    }
}
