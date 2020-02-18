using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : Singleton<CombatController>
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject weaponOnSpine;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private DealDamage dealDamage;
    [SerializeField]
    private SpellsController spellsController;
    
    private Timer timer;

    public bool blocking = false;

    private void Start()
    {
        timer = GetComponent<Timer>();
    }

    public bool inBattle = false;

    public void ToggleWeapon(string state)
    {
        weapon.SetActive(state == "true");
        weaponOnSpine.SetActive(!(state == "true"));
    }

    public void ToggleCombatMode()
    {
        inBattle = !inBattle;
        animator.SetBool("inBattle", inBattle);
        if (inBattle)
        {
            animator.SetTrigger("drawSword");
        }
        else
        {
            animator.SetTrigger("sheathSword");
        }
    }

    public void ToggleTriggers(bool state)
    {
        dealDamage.ToggleTriggers(state);
    }

    public void Attack()
    {
        if(!inBattle) 
        { 
            return;
        }

        dealDamage.Attack();
        timer.RestartTimer();
    }

    public void Block(bool state)
    {
        blocking = state;
        animator.SetBool("blocking", state);

        playerMovement.canMove = !state;
    }

    public void CastSpell()
    {
        spellsController.Cast();
    }

    public void ShootSpell()
    {
        spellsController.Shoot();
    }
}
