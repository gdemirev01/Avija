using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && GetComponent<PlayerMovement>().inBattle)
        {
            GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().Attack(gameObject);
        }
    }

    public void EndAttack()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().EndAttack();
    }

    public void EnableAttack()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().disableAttacks = false;
    }

    public void DisableAttack()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().disableAttacks = true;
    }
}
