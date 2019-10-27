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

    public void receiveHit(DealDamage other)
    {
        //lower health
        animator.SetTrigger("getHitted");
    }
}
