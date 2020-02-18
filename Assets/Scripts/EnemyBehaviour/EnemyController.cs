using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private DealDamage dealDamage;
    private Timer timer;

    private Transform target;
    private NavMeshAgent agent;

    public float lookRadius = 10f;
    public float rotationSpeed = 5f;

    [HideInInspector]
    public EnemySpawner spawner;

    private Vector3 direction;

    private void Awake()
    {
        direction = Vector3.zero;
    }

    void Start()
    {
        target = PlayerManager.Instance.player.transform.GetChild(0);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        dealDamage = GetComponentInChildren<DealDamage>();

        timer = GetComponent<Timer>();
        timer.onTimerRestart += OnTimerRestart;
        timer.onTimerEnd += OnTimerEnd;

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
            animator.SetBool("running", true);

            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool("running", false);

                FaceTarget();

                if(!timer.timerRunning)
                {
                    timer.RestartTimer();
                }
            }
            else
            {
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
        if (spawner == null)
        { 
            direction = transform.position + new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));
        }
        else
        {
            direction = transform.position + new Vector3(Random.Range(-spawner.size.x / 2, spawner.size.x / 2), Random.Range(-spawner.size.y / 2, spawner.size.y / 2), Random.Range(-spawner.size.z / 2, spawner.size.z / 2));
        }
    }

    public void EnableAttack()
    {
        dealDamage.ToggleTriggers(true);
    }

    public void DisableAttack()
    {
        dealDamage.ToggleTriggers(false);
    }

    public void OnTimerRestart()
    {
        animator.SetBool("attacking", false);
    }

    public void OnTimerEnd()
    {
        animator.SetBool("attacking", true);
    }
}
