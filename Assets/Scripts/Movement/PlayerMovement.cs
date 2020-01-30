using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;
    public DealDamage dealDamage;

    [SerializeField]
    private float speed;

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

    void Update()
    {
        InputMagnitude();
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

        if(inBattle)
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

        if (!blockRotationOnPlayer)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationspeed);
        }
    }

    private void InputMagnitude()
    {

        if(!canMove) { return; }

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
