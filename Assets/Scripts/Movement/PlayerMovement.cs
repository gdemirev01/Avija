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
    public Transform raycaster;
    public float verticalVelocity;

    private float previousHeight;
    private float currentHeight;

    public DealDamage dealDamage;

    public GameObject weapon;
    public GameObject weaponOnSpine;

    public bool inBattle
    {
        get; set;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();

        currentHeight = transform.position.y;

        var travel = System.Math.Round(currentHeight - previousHeight);
        if (travel < 0)
        {
            Debug.Log("falling");
        }
        previousHeight = currentHeight;
    }

    public void EnableMoving()
    {
        blockRotationOnPlayer = false;
        canMove = true;
    }

    public void DisableMoving()
    {
        blockRotationOnPlayer = true;
        canMove = false;
    }

    void BattleMovement()
    {

        if (!canMove) { return; }

        float InputX = Input.GetAxis("Horizontal");
        float InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        if (InputZ != 0 && InputX != 0)
        {
            moveDirection = forward + (right / 2) * InputX;
        }
        else 
        {
            moveDirection = forward;
        }

        if (!blockRotationOnPlayer)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed / 2);
        }
    }

    private void NormalMovement()
    {

        if (!canMove) { return; }

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
            raycaster.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), 1000);
        }
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

            if (inBattle)
            {
                BattleMovement();
            }
            else
            {
                NormalMovement();
            }
        } 
        else
        {
            animator.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
            animator.SetFloat("running", Input.GetAxis("Running"), 0.0f, Time.deltaTime);
        }
    }

    public void ChangeStance()
    {
        inBattle = !inBattle;
        if (inBattle)
        {
            animator.SetTrigger("drawSword");
        }
        else
        {
            animator.SetTrigger("sheathSword");
        }
    }

    public void ToggleWeapon(string state)
    {
        weapon.SetActive(state == "true");
        weaponOnSpine.SetActive(!(state == "true"));
    }
}
