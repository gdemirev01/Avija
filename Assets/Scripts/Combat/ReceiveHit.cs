using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveHit : MonoBehaviour
{
    public Animator animator;
    public CharacterProps characterProps;
    public QuestController questController;
    public EnemySpawner enemySpawner;

    public bool blocking = false;


    public void receiveHit(GameObject attacker)
    {
        if(blocking) { return; }
   
        var damage = attacker.GetComponent<WeaponStats>().damage;
        characterProps.health -= damage;
        if(characterProps.health <= 0)
        {
            animator.SetTrigger("die");

            if(this.tag.Equals("Player"))
            {
                GetComponent<CharacterController>().enabled = false;
                this.enabled = false;
            }
            else if(tag.Equals("Enemy"))
            {
                Die();
            }
        }

        animator.SetTrigger("getHitted");
    }

    public void Die()
    {
        GetComponent<EnemyController>().enabled = false;
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
        questController.SendProgressForQuest(GetComponent<CharacterProps>().name);
    }
}
