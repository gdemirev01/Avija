using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveHit : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void receiveHit(GameObject attacker)
    {
        if (gameObject.GetComponent<CharacterController>())
        {
            gameObject.GetComponent<CharacterController>().EndAttack();
            gameObject.GetComponent<CharacterController>().EnableAttack();
        }
        else if(gameObject.GetComponent<EnemyController>())
        {
            gameObject.GetComponent<EnemyController>().EnableAttack();
        }

        var damage = attacker.GetComponent<WeaponStats>().damage;
        GetComponent<CharacterProps>().health -= damage;
        var health = GetComponent<CharacterProps>().health;
        Debug.Log("health of " + this.tag + " " + health);
        if(health <= 0)
        {
            animator.SetTrigger("die");
            if(this.tag.Equals("Player"))
            {
                GetComponent<CharacterController>().enabled = false;
                this.enabled = false;
            }
            else if(tag.Equals("Enemy"))
            {
                GetComponent<EnemyController>().enabled = false;
                this.enabled = false;
                GetComponent<BoxCollider>().enabled = false;
            }
        }

        animator.SetTrigger("getHitted");
    }
}
