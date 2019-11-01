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

    public void receiveHit()
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

        //lower health
        animator.SetTrigger("getHitted");
    }
}
