using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    #region Singleton
    public static CombatController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is another instance of combatController");
            return;
        }

        instance = this;
    }
    #endregion


    public Animator animator;
    public GameObject weapon;
    public GameObject weaponOnSpine;
    public PlayerMovement playerMovement;
    public DealDamage dealDamage;
    public SpellsController spellsController;
    
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
        if(!inBattle) { return; }

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
