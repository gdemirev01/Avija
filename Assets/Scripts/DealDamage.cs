﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public bool isAttacking = false;
    private Animator animator;

    void Start()
    {
        animator = this.gameObject.transform.root.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag.Equals("Enemy") || other.gameObject.Equals("Player")) && isAttacking)
        {
            Debug.Log("hit");
            other.GetComponent<ReceiveHit>().receiveHit(this);
        }
    }

    public void Attack(GameObject attacker)
    {
        if (!isAttacking)
        {
            isAttacking = true;

            //animation
            animator.SetTrigger("attack");
        }
    }
}
