using System.Net;
using UnityEngine;

// Health management for both the player and enemies

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    XPManager XPManager;
    [SerializeField] bool isEnemy;
    [SerializeField] int XPValue = 1;

    void Awake()
    {
        XPManager = FindAnyObjectByType<XPManager>();
        Debug.Log("XP MANAGER:" + XPManager);
    }
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
                XPShard xpShard = Instantiate(XPManager.GetXPShardPrefab(), transform.position, Quaternion.identity);
                xpShard.SetXPValue(XPValue);
                Debug.Log("Enemy defeated, creating XP shard worth " + XPValue + " XP");
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
            if (isEnemy && other.CompareTag("Enemy")) return;   //if i am the enemy and the other tag is not the player then ignore
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
    }
}
