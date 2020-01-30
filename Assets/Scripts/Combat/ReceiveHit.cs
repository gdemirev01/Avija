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

        animator.SetTrigger("getHitted");

        var damage = attacker.GetComponent<CharacterProps>().damage;
        var armor = characterProps.armor;
        
        characterProps.health -= (damage - armor);

        if (characterProps.health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetTrigger("die");
        GetComponent<Collider>().enabled = false;

        if(this.tag.Equals("Player"))
        {
            //Load death screen
            return;
        }

        questController.SendProgressForQuest(GetComponent<CharacterProps>().name);

        Destroy(gameObject, 4f);
    }
}
