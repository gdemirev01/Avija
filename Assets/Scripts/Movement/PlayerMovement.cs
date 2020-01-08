using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Animator animator;
    public DealDamage dealDamage;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int acceleration = 1;

    [SerializeField]
    private float rotationspeed = 1;

    public bool canMove = true;
    public bool blockRotationOnPlayer = false;
    public float allowPlayerRotation;

    private Vector3 moveDirection;

    public GameObject weapon;
    public GameObject weaponOnSpine;

    public bool inBattle = false;
    public bool blocking = false;
    public bool inWater = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();
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
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationspeed / 2);
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

        if (!inWater)
        {
            forward.y = 0f;
        }
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        moveDirection = forward * InputZ + right * InputX;

        if(!blockRotationOnPlayer)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationspeed);
        }
    }

    private void InputMagnitude()
    {

        if(!canMove) { return; }

        float InputX = Input.GetAxis("Horizontal");
        float InputZ = Input.GetAxis("Vertical");

        animator.SetFloat("InputZ", InputZ, 0.0f, Time.deltaTime * 2f);
        animator.SetFloat("InputX", InputX, 0.0f, Time.deltaTime * 2f);
        animator.SetBool("inWater", inWater);

        speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if(speed > allowPlayerRotation)
        {
            animator.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
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
            animator.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
            animator.SetFloat("running", Input.GetAxis("Running"), 0.0f, Time.deltaTime);
        }
    }

    public void ChangeStance()
    {
        inBattle = !inBattle;
        animator.SetBool("inBattle", inBattle);
        if (inBattle)
        {
            animator.SetTrigger("drawSword");
        }
        else
        {
            animator.SetTrigger("sheathSword");
        }
    }

    public void Block(bool state)
    {
        blocking = state;
        animator.SetBool("blocking", state);

        canMove = !state;

        GameObject.Find("Collider").GetComponent<ReceiveHit>().blocking = state;
    }

    public void ToggleWeapon(string state)
    {
        weapon.SetActive(state == "true");
        weaponOnSpine.SetActive(!(state == "true"));
    }
}
