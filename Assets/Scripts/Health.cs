using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;

    private void Awake()
    {
        health = maxHealth;
    }

    protected virtual void Death()
    {
        Debug.Log("Death");
        GetComponent<Collider2D>().enabled = false;
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Death();
        }
    }

}
