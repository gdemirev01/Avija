using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public bool isAttacking = false;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.transform.root.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("attack");
            Invoke("NotAttacking", 1.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(animator.GetBool("attack"));
        if (other.gameObject.tag.Equals("Enemy") && isAttacking)
        {
            other.GetComponent<ReceiveHit>().receiveHit(gameObject);
        }
    }

    private void NotAttacking()
    {
        isAttacking = false;
    }
}
