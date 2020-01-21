using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public bool enableAttackTriggers = false;

    public Animator animator;

    private void Start()
    {
        if(this.tag.Equals("PlayerWeapon"))
        {
            animator = GameObject.Find("Player").GetComponent<Animator>();
        } 
        else if(this.tag.Equals("EnemyWeapon"))
        {
            animator = transform.root.GetComponent<Animator>();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (enableAttackTriggers)
        {
            if ((gameObject.tag.Equals("Player") && other.tag.Equals("Enemy")))
            {
                animator.ResetTrigger("getHitted");
                Debug.Log("this is " + this.name);
                Debug.Log("other is " + other.name);
                other.GetComponent<ReceiveHit>().receiveHit(gameObject);
            }
            else if ((gameObject.tag.Equals("Enemy") && other.tag.Equals("Player")))
            {
                animator.ResetTrigger("getHitted");
                other.GetComponent<ReceiveHit>().receiveHit(gameObject);
            }
        }
        

    }

    public void Attack()
    {
        animator.SetBool("attacking", true);
    }

    public void EndAttack()
    {
        animator.SetBool("attacking", false);
    }

    public void ToggleTriggers(bool state)
    {
        enableAttackTriggers = state;
    }
}
