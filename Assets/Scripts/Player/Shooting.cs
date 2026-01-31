using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform projectileSpawnPointRight;
    [SerializeField]
    Transform projectileSpawnPointLeft;

    [SerializeField]
    int ammoMax;
    int ammoCount;

    [SerializeField]
    float projectileForce = 15f;

    private void Awake()
    {
        ammoCount = ammoMax;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(ammoCount > 0)
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerToMouseDirection = mouseWorldPosition - new Vector2(transform.position.x, transform.position.y);

            if (playerToMouseDirection.x > 0)
            {
                projectile.transform.position = projectileSpawnPointRight.position;
            }
            else
            {
                projectile.transform.position = projectileSpawnPointLeft.position;
            }
            GameObject spawnedProjectile = Instantiate(projectile);
            spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(projectileForce * playerToMouseDirection.normalized);
            spawnedProjectile.GetComponent<Projectile>().directionNormalized = playerToMouseDirection.normalized;
            float angle = Mathf.Atan2(playerToMouseDirection.y, playerToMouseDirection.x) * Mathf.Rad2Deg;
            spawnedProjectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            --ammoCount;
        }
    }
}
