using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public bool isAttacking = false;
    public bool enableAttackTriggers = false;

    private Animator animator;
    private ComboSystem comboSystem;


    void Start()
    {
        animator = this.gameObject.transform.root.GetComponent<Animator>();
        if (GetComponent<ComboSystem>())
        {
            comboSystem = GetComponent<ComboSystem>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && enableAttackTriggers)
        {
            if ((gameObject.tag.Equals("PlayerWeapon") && other.gameObject.tag.Equals("Enemy")))
            {
                animator.ResetTrigger("getHitted");
                other.GetComponent<ReceiveHit>().receiveHit(gameObject);
            }
            else if ((gameObject.tag.Equals("EnemyWeapon") && other.gameObject.tag.Equals("Player")))
            {
                animator.ResetTrigger("getHitted");
                other.GetComponent<ReceiveHit>().receiveHit(gameObject);
            }
        }
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
        }
    }

    public void EndAttack()
    {
        animator.SetBool("attacking", false);
        isAttacking = false;
    }

    public void ToggleTriggers(bool state)
    {
        enableAttackTriggers = state;
    }
}
