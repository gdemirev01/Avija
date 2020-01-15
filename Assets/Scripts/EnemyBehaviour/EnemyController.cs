using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    public DealDamage dealDamage;
    public ComboSystem comboSystem;

    Transform target;
    NavMeshAgent agent;

    public float lookRadius = 10f;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animator.SetBool("running", true);

            if(distance <= agent.stoppingDistance)
            {
                animator.SetBool("running", false);
                FaceTarget();
                //Attack
            }
        }
        else
        {
            animator.SetBool("running", false);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //private void AdvanceToPlayer()
    //{
    //    if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
    //    {
    //        animator.SetBool("running", false);
    //        return;
    //    }
    //    else
    //    {
    //        animator.SetBool("running", true);
    //    }

    //    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    //    transform.LookAt(player.transform);
    //}

    //private void Attack()
    //{
    //    dealDamage.Attack();
    //    isAttacking = true;
    //    Invoke("EndAttack", cooldown);
    //}

    public void EndAttackAnimation()
    {
        animator.SetBool("attacking", false);
    }

    //private void EndAttack()
    //{
    //    isAttacking = false;
    //}

    //private void Detection()
    //{
    //    var distance = Vector3.Distance(transform.position, player.transform.position);
    //    if (distance < range)
    //    {
    //        playerInRange = true;
    //    }
    //    else
    //    {
    //        playerInRange = false;
    //    }
    //}

    public void EnableAttack()
    {
        dealDamage.enableAttackTriggers = true;
    }

    public void DisableAttack()
    {
        dealDamage.enableAttackTriggers = false;
    }
}
