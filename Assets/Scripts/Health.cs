using System.Net;
using UnityEngine;

// Health management for both the player and enemies

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] bool isEnemy;
    [SerializeField] float XPValue = 1f;
    XPManager XPManager;

    void Awake()
    {
        XPManager = FindAnyObjectByType<XPManager>();
        Debug.Log("XP MANAGER:" + XPManager);
    }
    public float GetHealth() => health;

    public void TakeDamage(float damage, DamageDealer dealer = null)
    {
        if (dealer != null)
        {
            if (isEnemy && dealer.IsFromEnemy()) return;
            if (!isEnemy && !dealer.IsFromEnemy()) return;
        }


        health -= damage;
        if (health <= 0)
        {
            if (isEnemy && XPManager != null)
            {
                XPShard xpShard = Instantiate(XPManager.GetXPShardPrefab(), transform.position, Quaternion.identity);
                xpShard.SetXPValue(XPValue);
                Debug.Log("Enemy defeated, creating XP shard worth " + XPValue + " XP");
            }
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            // Enemy bullet → Player
            if (damageDealer.IsFromEnemy() && !isEnemy)
            {
                TakeDamage(damageDealer.GetDamage(), damageDealer);
                damageDealer.Hit();
            }
            // Player bullet → Enemy
            else if (!damageDealer.IsFromEnemy() && isEnemy)
            {
                TakeDamage(damageDealer.GetDamage(), damageDealer);
                damageDealer.Hit();
            }
            return; // done if it's a projectile
        }

        // Handle physical enemy <-> player collisions
        if (isEnemy && other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10f); // enemy body deals damage to player
                Die(); // enemy dies after collision
            }
        }
    }
}
