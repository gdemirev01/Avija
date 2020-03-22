using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private CombatController combatController;
    private EnemyInfo enemyInfo;

    private int enemiesCount = 0;

    private void Start()
    {
        enemyInfo = EnemyInfo.Instance;
        combatController = CombatController.Instance;
    }

    private void Update()
    {
        if(enemiesCount == 0)
        {
            enemyInfo.TogglePanel(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy"))
        {
            enemiesCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            enemiesCount--;
        }
    }
}
