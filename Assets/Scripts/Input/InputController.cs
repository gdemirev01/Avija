using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : Singleton<InputController>
{
    private PlayerMovement playerMovement;
    private CombatController combatController;
    private UIController uiController;
    private EnemyInfo enemyInfo;

    public Animator animator;
    public DealDamage dealDamage;

    private bool togglePointer = false;

    void Start()
    {
        playerMovement = transform.GetChild(0).GetComponent<PlayerMovement>();
        uiController = UIController.Instance;
        combatController = CombatController.Instance;
        enemyInfo = EnemyInfo.Instance;
    }

    void Update()
    {
        UIInput();
        CombatInput();
        MovementInput();
    }

    private void MovementInput()
    {
        if (GetPointerState())
        { 
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("jump");
        }
    }

    private void CombatInput()
    {
        if(GetPointerState())
        { 
            return;
        }

        if (Input.GetButtonDown("Attack"))
        {
            combatController.Attack();
        }

        if (Input.GetButtonDown("DrawSword"))
        {
            combatController.ToggleCombatMode();
            enemyInfo.TogglePanel(false);
        }

        if (Input.GetButtonDown("Block"))
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

    public bool GetPointerState()
    {
        return Cursor.visible;
    }
}
