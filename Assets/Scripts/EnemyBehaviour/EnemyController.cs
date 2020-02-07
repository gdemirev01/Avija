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
    public float rotationSpeed = 5f;

    public EnemySpawner spawner;

    Vector3 direction;


    void Start()
    {
        target = PlayerManager.instance.player.transform.GetChild(0);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        dealDamage = GetComponent<DealDamage>();

        InvokeRepeating("RandomMovement", 1f, Random.Range(10, 20));
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
            animator.SetBool("walking", false);

            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool("running", false);
                FaceTarget();
                dealDamage.Attack();
            }
            else
            {
                animator.SetBool("running", true);
                dealDamage.EndAttack();
            }
        }
        else
        {
            animator.SetBool("running", false);
            animator.SetBool("walking", true);
            agent.SetDestination(direction);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void RandomMovement()
    {
        direction = transform.position + new Vector3(Random.Range(-spawner.size.x / 2, spawner.size.x / 2), Random.Range(-spawner.size.y / 2, spawner.size.y / 2), Random.Range(-spawner.size.z / 2, spawner.size.z / 2));
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
