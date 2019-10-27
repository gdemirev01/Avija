using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    private Animator animator;
    public float speed = 5f;
    public bool isMoving = false;
    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if(Input.GetMouseButtonDown(0))
        {
            GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().Attack(gameObject);
        }
    }

    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        if(!canMove) { return; }

        var acceleration = 1;
        if (Input.GetButtonDown("Running"))
        {
            acceleration = 2;
            animator.SetBool("running", true);
        }
        if(Input.GetButtonUp("Running"))
        {
            animator.SetBool("running", false);
        }

        if (hor != 0)
        {
            animator.SetFloat("direction", Mathf.Ceil(1 * hor));
            animator.SetBool("strifing", true);
        } 
        else
        {
            animator.SetBool("strifing", false);
        }

        if(ver != 0)
        {
            animator.SetFloat("direction", Mathf.Ceil(1 * ver));
            animator.SetBool("walking", true);
        } else
        {
            
            animator.SetBool("walking", false);
        }

        

        Vector3 playerMovement = new Vector3(hor, 0f, ver) * speed * acceleration * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }

    public void StopAnimation()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().isAttacking = false;
    }

    public void EnableMoving()
    {
        canMove = true;
    }
}
