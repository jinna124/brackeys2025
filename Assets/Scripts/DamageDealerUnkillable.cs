using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    // Doesn't do anything, is just there to mark the object as a damage dealer and enable access from other scripts
    [SerializeField] public float damage = 10f;
    [SerializeField] bool destroyOnHit = true;
    [SerializeField] bool isFromEnemy = false;
    //private bool isHit = false;
    private HashSet<GameObject> enemiesHit = new HashSet<GameObject>();     // array holding the enemies hit
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

    private HashSet<GameObject> objectsHit = new HashSet<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        // Player bullet hits enemy
        if (collision.tag == "Enemy" && !objectsHit.Contains(target) && !isFromEnemy)
        {
            objectsHit.Add(target);
            Health health = target.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(GetDamage(), this);
            }
            Hit();
        }
        // Enemy bullet hits player
        else if (isFromEnemy && collision.tag == "Player" && !objectsHit.Contains(target))
        {
            objectsHit.Add(target);
            Health health = target.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(GetDamage(), this);
            Hit();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        if (objectsHit.Contains(target))
            objectsHit.Remove(target);
    }

}
