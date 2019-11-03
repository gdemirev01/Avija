using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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

    public void DisableAttackTriggers()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().enableAttackTriggers = false;
    }

    public void EnableAttackTriggers()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().enableAttackTriggers = true;
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
