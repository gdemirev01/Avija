using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveHit : MonoBehaviour
{
    private Animator animator;
    private CharacterProps characterProps;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterProps = GetComponent<CharacterProps>();
    }

    public void receiveHit(GameObject attacker)
    {
        if (gameObject.GetComponent<CharacterController>())
        {
            gameObject.GetComponent<AnimationEvents>().EndAttack();
            gameObject.GetComponent<AnimationEvents>().DisableAttackTriggers();
            gameObject.GetComponent<AnimationEvents>().ResetAttackTriggger();
        }
        else if(gameObject.GetComponent<EnemyController>())
        {
            gameObject.GetComponent<EnemyController>().EnableAttack();
        }

        var damage = attacker.GetComponent<WeaponStats>().damage;
        characterProps.health -= damage;
        if(characterProps.health <= 0)
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
                GetComponent<BoxCollider>().enabled = false;
                this.enabled = false;
            }
        }

        animator.SetTrigger("getHitted");
    }
}
