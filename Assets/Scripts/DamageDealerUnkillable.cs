using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    // Doesn't do anything, is just there to mark the object as a damage dealer and enable access from other scripts
    [SerializeField] float damage = 10f;
    [SerializeField] bool destroyOnHit = true;
    [SerializeField] bool isFromEnemy = false;
    //private bool isHit = false;
    private HashSet<Collider2D> enemiesHit = new HashSet<Collider2D>();     // array holding the enemies hit
    private PlayerStats playerstats;
    private GameObject player; // assumes player has "Player" tag
    public void Awake()
    {
        player = GameObject.FindWithTag("Player");
        if (tag == "PlayerBullet")
        {
            if (player != null)
            {
                playerstats = player.GetComponent<PlayerStats>();
                damage = playerstats.getWeaponDamage();
            }
        }

    }

    public float GetDamage () => damage;
    public bool IsFromEnemy() => isFromEnemy;
    public void Hit()
    {
        if(destroyOnHit)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !(enemiesHit.Contains(collision)))       // checks if the collision is a new enemy not hit before
        {
            enemiesHit.Add(collision);
            Health health = collision.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(GetDamage(), this);
            }
            Hit();
        }

        else if (isFromEnemy && collision.tag == "Player")
        {
            Health health = collision.GetComponent<Health>();
            if(health != null)
                health.TakeDamage(GetDamage(), this);
            Hit();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && (enemiesHit.Contains(collision)))
        {
            enemiesHit.Remove(collision);
        }
    }
}
