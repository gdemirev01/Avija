using UnityEngine;

public class ReceiveHit : MonoBehaviour
{
    public Animator animator;
    public CharacterProps characterProps;

    private QuestController questController;
    private EnemySpawner enemySpawner;

    private CombatController combatController;

    private void Start()
    {
        questController = QuestController.instance;
        combatController = CombatController.instance;

        if(tag.Equals("Enemy"))
        {
            enemySpawner = GetComponent<EnemyController>().spawner;
        }
    }

    public void receiveHit(GameObject attacker)
    { 
        animator.SetTrigger("getHitted");

        var damage = attacker.GetComponent<CharacterProps>().damage;
        var armor = characterProps.armor;

        if (combatController.blocking) { damage /= 4; }

        damage -= armor;
        if(damage <= 0) { damage = 0; }

        characterProps.health -= damage;

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
        else if(tag.Equals("Enemy"))
        {
            enemySpawner.SpawnEnemy(1);
        }

        questController.SendProgressForQuest(GetComponent<CharacterProps>().name);

        Destroy(gameObject, 4f);
    }
}
