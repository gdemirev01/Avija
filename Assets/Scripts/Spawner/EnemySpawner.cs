using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Vector3 size;

    public GameObject enemyPrefab;

    public void SpawnEnemy(int count)
    {
        Vector3 position;
        for (int i = 0; i <= count; ++i)
        {
            position = transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemy.GetComponent<EnemyController>().spawner = this;
        }
    }

    private void Start()
    {
        SpawnEnemy(1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, size);
    }
}
