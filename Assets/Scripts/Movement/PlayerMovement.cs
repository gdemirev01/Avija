using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    private Animator animator;
    private CombatController combatController;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationspeed = 1;
    private Vector3 moveDirection;

    public bool canMove = true;
    public float allowPlayerRotation;

    private void Start()
    {
        combatController = CombatController.Instance;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        InputMagnitude();
    }

    private void NormalMovement()
    {

        if (!canMove)
        { 
            return; 
        }

        float InputX = Input.GetAxis("Horizontal");
        float InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        if(combatController.inBattle)
        {
            if (InputZ != 0 && InputX != 0)
            {
                moveDirection = forward + (right / 2) * InputX;
            }
            else
            {
                moveDirection = forward;
            }
        }
        else
        {
            moveDirection = forward * InputZ + right * InputX;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationspeed);
    }

    private void InputMagnitude()
    {
        if(!canMove)
        {
            animator.SetFloat("inputMagnitude", 0);
            animator.SetFloat("running", 0);
            return; 
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        float running = Input.GetAxis("Running");

        animator.SetFloat("inputZ", inputZ);
        animator.SetFloat("inputX", inputX);

        speed = new Vector2(inputX, inputZ).sqrMagnitude;
        if(speed > allowPlayerRotation)
        {
            animator.SetFloat("inputMagnitude", speed);
            animator.SetFloat("running", running);

            NormalMovement();
        } 
        else
        {
            animator.SetFloat("inputMagnitude", speed);
            animator.SetFloat("running", running);
        }
    }
}
