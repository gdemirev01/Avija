using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    public float range = 5;
    public bool playerInRange = false;
    public bool attacking = false;
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

        if(attacking && playerInRange)
        {
            StartCoroutine(AttackDelay());
        }
    }

    private void AdvanceToPlayer()
    {
        

        if (Vector3.Distance(transform.position, player.transform.position) < 1f)
        {
            animator.SetBool("running", false);
            attacking = true;
            return;
        }
        else
        {
            animator.SetBool("running", true);
            attacking = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.LookAt(player.transform);
    }

    private void Detection()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 15f)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    IEnumerator AttackDelay()
    {
        attacking = false;

        yield return new WaitForSeconds(3f);

        GameObject.Find("LeftHand").GetComponent<DealDamage>().Attack(gameObject);

        attacking = true;
    }

    public void EnableAttacking()
    {
        GameObject.Find("LeftHand").GetComponent<DealDamage>().isAttacking = false;
    }
}
