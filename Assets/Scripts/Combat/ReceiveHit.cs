using UnityEngine;
using UnityEngine.SceneManagement;

public class ReceiveHit : MonoBehaviour
{
    public Animator animator;
    public CharacterProps characterProps;

    private EnemyInfo enemyInfo;

    private EnemySpawner enemySpawner;
    private QuestController questController;
    private CombatController combatController;

    private void Start()
    {
        enemyInfo = EnemyInfo.Instance;
        questController = QuestController.Instance;
        combatController = CombatController.Instance;

        if(tag.Equals("Enemy"))
        {
            enemySpawner = GetComponent<EnemyController>().spawner;
        }
    }

    public void GetHit(GameObject attacker)
    { 
        animator.SetTrigger("getHit");

        var attackerProps = attacker.GetComponent<CharacterProps>();

        if(attacker.tag.Equals("Enemy"))
        {
            enemyInfo.SetEnemy(attackerProps);
        }
        else if(attacker.tag.Equals("Player"))
        {
            enemyInfo.SetEnemy(characterProps);
        }
        enemyInfo.TogglePanel(true);

        var damage = attackerProps.damage;
        var armor = characterProps.armor;

        if (combatController.blocking) { damage /= 4; }

        damage -= armor;
        if(damage <= 0) 
        { 
            damage = 0; 
        }

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
            SceneManager.LoadScene("DeathScreen", LoadSceneMode.Single);
            return;
        }
        else if(tag.Equals("Enemy"))
        {
            enemyInfo.TogglePanel(false);
            enemySpawner.SpawnEnemy(1);
        }

        questController.SendProgressForQuest(GetComponent<CharacterProps>().name);

        Destroy(gameObject, 4f);
    }
}
