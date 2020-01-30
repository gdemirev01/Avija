using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private DealDamage dealDamage;

    Transform target;
    NavMeshAgent agent;

    public float lookRadius = 10f;

    void Start()
    {
        target = PlayerManager.instance.player.transform.GetChild(0);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        dealDamage = GetComponent<DealDamage>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if(agent.isOnOffMeshLink)
        {
            animator.SetTrigger("jump");
        }
        else
        {
            animator.ResetTrigger("jump");
        }

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animator.SetBool("running", true);

            if(distance <= agent.stoppingDistance)
            {
                animator.SetBool("running", false);
                FaceTarget();
                dealDamage.Attack();
            }
            else
            {
                dealDamage.EndAttack();
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

    public void EnableAttack()
    {
        dealDamage.enableAttackTriggers = true;
    }

    public void DisableAttack()
    {
        dealDamage.enableAttackTriggers = false;
    }
}
