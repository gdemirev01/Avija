using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private float Speed;
    public bool canMove = true;
    public int acceleration = 1;
    private Animator animator;
    public float rotationSpeed = 1;
    public Vector3 moveDirection;
    public bool blockRotationOnPlayer = false;
    public float allowPlayerRotation;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<CharacterController>().inBattle)
        {
            BattleMovement();
        } 
        else
        {
            InputMagnitude();
        }


        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("dancing");
        }
    }

    public void EnableMoving()
    {
        canMove = true;
    }

    public void DisableMoving()
    {
        canMove = false;
    }

    void BattleMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        if (!canMove) { return; }

        if (hor != 0)
        {
            animator.SetFloat("direction", Mathf.Ceil(1 * hor));
            animator.SetBool("strifing", true);
        }
        else
        {
            animator.SetBool("strifing", false);
        }

        if (ver != 0)
        {
            animator.SetFloat("direction", Mathf.Ceil(1 * ver));
            animator.SetBool("walking", true);
        }
        else
        {

            animator.SetBool("walking", false);
        }

        if (ver < 0) { hor *= -1; }

        Vector3 playerMovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }

    private void NormalMovement()
    {
        float InputX = Input.GetAxis("Horizontal");
        float InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        moveDirection = forward * InputZ + right * InputX;

        if(!blockRotationOnPlayer)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed);
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void InputMagnitude()
    {
        float InputX = Input.GetAxis("Horizontal");
        float InputZ = Input.GetAxis("Vertical");

        animator.SetFloat("InputZ", InputZ, 0.0f, Time.deltaTime * 2f);
        animator.SetFloat("InputX", InputX, 0.0f, Time.deltaTime * 2f);

        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if(Speed > allowPlayerRotation)
        {
            animator.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
            animator.SetFloat("running", Input.GetAxis("Running"), 0.0f, Time.deltaTime);
            NormalMovement();
        } 
        else
        {
            animator.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
        }
    }
}
