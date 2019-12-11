using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveHit : MonoBehaviour
{
    private Animator animator;
    private CharacterProps characterProps;
    public QuestController questController;
    public ComboSystem comboSystem;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterProps = GetComponent<CharacterProps>();
    }

    public void receiveHit(GameObject attacker)
    {
        if (tag.Equals("Player"))
        {
            Debug.Log(tag);
            gameObject.GetComponent<AnimationEvents>().EndAttack();
            gameObject.GetComponent<AnimationEvents>().DisableAttackTriggers();
            gameObject.GetComponent<AnimationEvents>().ResetAttackTriggger();
            comboSystem.ResetStreak();
        }
        else if(gameObject.GetComponent<EnemyController>())
        {
            gameObject.GetComponent<EnemyController>().EnableAttack();
        }

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
