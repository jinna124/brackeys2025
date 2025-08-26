using System.Net;
using UnityEngine;

// Health management for both the player and enemies

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    CookieManager cookieManager;
    [SerializeField] bool isEnemy;
    [SerializeField] int cookieValue = 1;

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (isEnemy)
            {
                cookieManager.AddCookies(cookieValue);
                Die();
            }
            else
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
    }
}
