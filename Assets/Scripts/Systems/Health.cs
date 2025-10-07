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
    [SerializeField] SpriteRenderer spriterenderer;
    [SerializeField] float time_of_red = 0.5f;
    [SerializeField] CameraShake camerashake;
    public float currentHealth;
    private PlayerStats playerstats;
    XPManager XPManager;
    private Animator animator;
    private bool isDead = false;
    public bool isFrozen = false;
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

        if (tag == "Enemy")
        {
            animator = GetComponent<Animator>();
            spriterenderer = GetComponent<SpriteRenderer>();
        }
    }
    public float GetHealth() => currentHealth;
    public float GetMaxHealth() => Maxhealth;
    public void TakeDamage(float damage, DamageDealer dealer = null)
    {
        if (isDead) return;
        if (dealer != null)
        {
            if (isEnemy && dealer.IsFromEnemy()) return;
            if (!isEnemy && !dealer.IsFromEnemy()) return;
        }

        if (!isEnemy) currentHealth -= damage / 7f;
        else currentHealth -= damage;
        // these are for the effects
        if (!isEnemy && spriterenderer != null && camerashake != null) 
        {
            StartCoroutine(PlayerGetHit());
        }
        else if(isEnemy && spriterenderer != null && animator != null)
        {
            StartCoroutine(EnemyGetHit());
        }

        if (currentHealth <= 0)
        {
            isDead = true;
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
                Debug.Log("Enemy Health: " + currentHealth);
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
        SceneSwitcher sceneSwitcher = SceneSwitcher.instance;
        sceneSwitcher.LoadGameOver();
        Destroy(gameObject);
    }

    IEnumerator PlayerGetHit()
    {
        spriterenderer.color = new Color(1f, 0.5f, 0.5f);
        animator.Play("GrannyHit");
        camerashake.Shake();

        // pause the game for a brief moment when the player gets hit
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.03f);
        Time.timeScale = 1f;

        yield return new WaitForSeconds(time_of_red);
        spriterenderer.color = Color.white;
    }

    IEnumerator EnemyGetHit()
    {
        spriterenderer.color = new Color(1f, 0.5f, 0.5f);
        isFrozen = true;
        animator.speed = 0f;

        yield return new WaitForSecondsRealtime(0.08f);

        isFrozen = false;
        animator.speed = 1f;

        yield return new WaitForSeconds(time_of_red);
        spriterenderer.color = Color.white;
    }
}