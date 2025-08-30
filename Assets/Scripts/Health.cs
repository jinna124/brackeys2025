using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// Health management for both the player and enemies

public class Health : MonoBehaviour
{
    [SerializeField] float Maxhealth = 100f;
    [SerializeField] bool isEnemy;
    [SerializeField] float XPValue = 1f;
    XPManager XPManager;
    private float currentHealth;
    private PlayerStats playerstats;
    private Animator animator;
    void Awake()
    {
        if (tag == "Player")
        {
            playerstats = GetComponent<PlayerStats>();
            Maxhealth = playerstats.getMaxHp();
            currentHealth = Maxhealth;
            animator = GetComponent<Animator>();
        }
        currentHealth = Maxhealth;
        XPManager = FindAnyObjectByType<XPManager>();
        Debug.Log("XP MANAGER:" + XPManager);
    }
    public float GetHealth() => currentHealth;

    public void TakeDamage(float damage, DamageDealer dealer = null)
    {
        if (dealer != null)
        {
            if (isEnemy && dealer.IsFromEnemy()) return;
            if (!isEnemy && !dealer.IsFromEnemy()) return;
        }


        currentHealth -= damage;
        if (!isEnemy) { animator.Play("GrannyHit"); }
        if (currentHealth <= 0)
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
        if (!isEnemy)
        {
            StartCoroutine(playerDeathAnimation());
        }
        else
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

    public void addMaxHp(float addedvalue)
    {
        Maxhealth += addedvalue;
    }
    public void Fullheal()
    {
        currentHealth = Maxhealth;
    }

    IEnumerator playerDeathAnimation()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Collider2D col = rb.GetComponent<Collider2D>();
        rb.linearVelocity = Vector3.zero;
        rb.simulated = false;
        col.enabled = false;
        animator.Play("GrannyDies");
        yield return null;
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float Length = animatorStateInfo.length;
        // Wait for the length of the animation
        yield return new WaitForSeconds(Length + 5);
        Destroy(gameObject);
    }

    IEnumerator playerHitAnimation()
    {
        animator.SetBool("isHit", true);
        yield return null;
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float Length = animatorStateInfo.length;
        // Wait for the length of the animation
        yield return new WaitForSeconds(Length);
        animator.SetBool("isHit", false);
    }
}
