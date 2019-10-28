using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{

    public bool inBattle = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().Attack(gameObject);
        }
    }



    public void StopAnimation()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().isAttacking = false;
    }
}
