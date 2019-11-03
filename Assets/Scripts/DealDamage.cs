using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public bool isAttacking = false;
    public bool enableAttackTriggers = false;
    private Animator animator;
    private int comboStreak = 1;
    public float timeLeft;
    private bool timerRunning = false;


    void Start()
    {
        animator = this.gameObject.transform.root.GetComponent<Animator>();
    }

    private void Update()
    {
        if(timerRunning)
        {
            UpdateTimer();
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
            Debug.Log("attack");
            isAttacking = true;

            if (tag.Equals("PlayerWeapon"))
            {
                animator.SetInteger("comboStreak", comboStreak);
                comboStreak++;

                if (comboStreak > 3)
                {
                    comboStreak = 3;
                }

            }
            animator.SetTrigger("attack");
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    public void EnableTriggers()
    {
        enableAttackTriggers = true;
    }

    public void DisableTriggers()
    {
        enableAttackTriggers = false;
    }

    private void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            comboStreak = 1;

            animator.SetInteger("comboStreak", comboStreak);
            animator.ResetTrigger("attack");
            isAttacking = false;
            timerRunning = false;
            timeLeft = 1.5f;
        }
    }

    public void ResetCombo()
    {
        comboStreak = 1;
        animator.SetInteger("comboStreak", comboStreak);
    }

    public void ResetAttackTrigger()
    {
        animator.ResetTrigger("attack");
        isAttacking = false;
    }

    public void RestartTimer()
    {
        timeLeft = 1.5f;
        timerRunning = true;
    }
}
