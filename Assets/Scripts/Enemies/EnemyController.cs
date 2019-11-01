using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    public float range = 5;
    public bool playerInRange = false;
    public bool playerInAttackRange = false;
    public float speed = 1f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Detection();

        if (playerInRange)
        {
            AdvanceToPlayer();
        } 
        else
        {
            animator.SetBool("running", false);
        }

        if(playerInRange && playerInAttackRange)
        {
            if(!GameObject.Find("LeftHand").GetComponent<DealDamage>().isAttacking)
            {
                Attack();
            }
        }
    }

    private void AdvanceToPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
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
        GameObject.Find("LeftHand").GetComponent<DealDamage>().Attack();
        Invoke("EndAttack", 5f);
    }

    private void Detection()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    public void EndAttack()
    {
        GameObject.Find("LeftHand").GetComponent<DealDamage>().EndAttack();
    }

    public void EnableAttack()
    {
        GameObject.Find("LeftHand").GetComponent<DealDamage>().disableAttacks = false;
    }

    public void DisableAttack()
    {
        GameObject.Find("LeftHand").GetComponent<DealDamage>().disableAttacks = true;
    }
}
