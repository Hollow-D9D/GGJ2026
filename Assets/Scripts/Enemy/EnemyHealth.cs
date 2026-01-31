using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    protected override void Death()
    {
        Debug.Log(name + ": dead");
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.GetComponent<Projectile>();
        if(projectile != null)
        {
            DealDamage(projectile.damage);
            Destroy(projectile.gameObject);
            Debug.Log("ax");
        }
        else
        {

        }
    }
}
