using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public bool isAttacking = false;
    private Animator animator;
    public bool disableAttacks = false;

    void Start()
    {
        animator = this.gameObject.transform.root.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && !disableAttacks)
        {
            if ((gameObject.tag.Equals("PlayerWeapon") && other.gameObject.tag.Equals("Enemy")))
            {
                animator.ResetTrigger("getHitted");
                other.GetComponent<ReceiveHit>().receiveHit();
            }
            else if ((gameObject.tag.Equals("EnemyWeapon") && other.gameObject.tag.Equals("Player")))
            {
                animator.ResetTrigger("getHitted");
                other.GetComponent<ReceiveHit>().receiveHit();
            }
        }
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;

            animator.SetTrigger("attack");
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }
}
