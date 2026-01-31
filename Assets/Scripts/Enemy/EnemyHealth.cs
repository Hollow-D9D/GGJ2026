using EnemyPatrolling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] SpriteRenderer displayHealth;

    protected override void Death()
    {
        GetComponent<DeathEnemy>().DoDeath();
        Destroy(displayHealth.gameObject, .5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.GetComponent<Projectile>();
        if(projectile != null)
        {
            DealDamage(projectile.damage);
            Destroy(projectile.gameObject);
        }
    }

    protected override void DamageDealth(int damage)
    {
        displayHealth.size = new Vector2(displayHealth.size.x, (float)health / (float)maxHealth);
        displayHealth.transform.localPosition = new Vector3(displayHealth.transform.localPosition.x, (((float)(maxHealth - health) / (float)maxHealth) / 2f), displayHealth.transform.localPosition.z);
    }
}
