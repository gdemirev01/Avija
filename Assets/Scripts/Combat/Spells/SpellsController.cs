using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsController : MonoBehaviour
{

    public GameObject projectilePrefab;

    public Transform spawnPosition;

    private GameObject projectile;

    public void Cast()
    {
        projectile = Instantiate(projectilePrefab, spawnPosition);
    }

    public void Shoot()
    {
        projectile.transform.parent = null;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = transform.forward * 100;

        Destroy(projectile, 1f);
    }
}
