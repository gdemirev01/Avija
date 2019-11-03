using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && GetComponent<PlayerMovement>().inBattle)
        {
            GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().Attack();
            GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().RestartTimer();
        }
    }

    public void EndAttack()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().EndAttack();
    }

    public void EnableAttackTriggers()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().ToggleTriggers(true);
    }

    public void DisableAttackTriggers()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().ToggleTriggers(false);
    }

    public void ResetComboStreak()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().ResetCombo();
    }

    public void ResetAttackTriggger()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().ResetAttackTrigger();
    }
}
