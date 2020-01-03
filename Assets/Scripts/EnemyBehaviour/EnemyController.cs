using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float range = 5;
    public bool playerInRange = false;
    public bool playerInAttackRange = false;
    public float speed = 1f;
    private Animator animator;
    public bool canMove = true;
    public DealDamage dealDamage;
    private float attackRange = 15f;

    private bool isAttacking = false;

    public ComboSystem comboSystem;

    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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
