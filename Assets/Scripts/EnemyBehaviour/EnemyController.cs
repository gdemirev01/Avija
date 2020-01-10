using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    public DealDamage dealDamage;
    public ComboSystem comboSystem;

    public float range = 5;
    public float speed = 1f;
    public float attackRange = 15f;
    public float cooldown;

    private bool isAttacking = false;
    public bool canMove = true;
    public bool playerInRange = false;
    public bool playerInAttackRange = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Detection();

        if (playerInRange && playerInAttackRange && !isAttacking)
        {
            Attack();
        }

        if (playerInRange)
        {
            AdvanceToPlayer();
        } 
        else
        {
            animator.SetBool("running", false);
        }
    }

    private void AdvanceToPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            animator.SetBool("running", false);
            playerInAttackRange = true;
            return;
        }
        else
        {
            animator.SetBool("running", true);
        }

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.LookAt(player.transform);
    }

    private void Attack()
    {
        dealDamage.Attack();
        isAttacking = true;
        Invoke("EndAttack", cooldown);
    }

    public void EndAttackAnimation()
    {
        animator.SetBool("attacking", false);
    }

    private void EndAttack()
    {
        isAttacking = false;
    }

    private void Detection()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < range)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    public void EnableAttack()
    {
        dealDamage.enableAttackTriggers = true;
    }

    public void DisableAttack()
    {
        dealDamage.enableAttackTriggers = false;
    }
}
