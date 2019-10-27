using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    private Animator animator;
    public float speed = 5f;
    public bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

    
    }

    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");


        if(hor != 0)
        {
            if(hor < 0)
            {
                animator.SetFloat("direction", -1F);
            }
            else
            {
                animator.SetFloat("direction", 1F);
            }
            animator.SetBool("strifing", true);
        } 
        else
        {
            animator.SetBool("strifing", false);
        }

        if(ver != 0)
        {
            if (ver < 0)
            {
                animator.SetFloat("direction", -1F);
            }
            else
            {
                animator.SetFloat("direction", 1F);
            }
            animator.SetBool("walking", true);
        } else
        {
            
            animator.SetBool("walking", false);
        }

        Vector3 playerMovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }

    public void StopAnimation()
    {
        animator.SetBool("attacking", false);
    }
}
