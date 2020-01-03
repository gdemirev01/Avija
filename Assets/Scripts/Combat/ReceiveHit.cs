using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveHit : MonoBehaviour
{
    private Animator animator;
    private CharacterProps characterProps;
    public QuestController questController;

    public bool blocking = false;

    void Start()
    {
        animator = transform.root.GetComponent<Animator>();
        characterProps = transform.root.GetComponent<CharacterProps>();
    }

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
                GetComponent<EnemyController>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
                this.enabled = false;
                questController.SendProgressForQuest(GetComponent<CharacterProps>().name);
            }
        }

        animator.SetTrigger("getHitted");
    }
}
