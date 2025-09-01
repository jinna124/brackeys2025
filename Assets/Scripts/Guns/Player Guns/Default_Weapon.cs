using System.Collections;
using UnityEngine;

public class Default_Weapon : MonoBehaviour
{

    [Header("Weapon Settings")]
    [Space]
    [SerializeField] GameObject tip_of_weapon;
    //[SerializeField] float tweaking_angle;
    [Space]

    [Header("Projectile Settings")]
    [Space]
    [SerializeField] GameObject bullet;
    [SerializeField] float projectile_speed;
    [SerializeField] float projectile_lifetime;
    [Space]
    [Header("____Firing rate and range")]
    [SerializeField] float firing_rate;
    [SerializeField] float firing_range = 6f;
    //[SerializeField] float firing_variance;           // these are not implemented yet
    //[SerializeField] float minimum_firing_rate;
    private GameObject nearest_enemy;
    private bool isFiring = false;


    IEnumerator StartShooting()
    {
        isFiring = true;
        nearest_enemy = EnemyManager.instance.GetNearestEnemy(tip_of_weapon.transform.position);     // find nearest enemy per bullet
        if(nearest_enemy != null ) 
            AimAndShootAutomatically(nearest_enemy);
        yield return new WaitForSeconds(firing_rate);   // this makes it so that each bullet that gets out depends on the nearest enemy
        isFiring = false;
    }
    void AimAndShootAutomatically(GameObject enemy)
    {

        // -----------bullet-------------
        Vector2 direction = (enemy.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2 (-direction.y, -direction.x) * Mathf.Rad2Deg + 90f;
        GameObject bullet_instance = Instantiate(bullet, tip_of_weapon.transform.position, Quaternion.Euler(0f, 0f, angle));
        Rigidbody2D bullet_rb = bullet_instance.GetComponent<Rigidbody2D>();
        bullet_rb.linearVelocity = direction * projectile_speed;
        Destroy(bullet_instance, projectile_lifetime);
        // -------------------------------
    }
    void Update()
    {
        nearest_enemy = EnemyManager.instance.GetNearestEnemy(tip_of_weapon.transform.position);

        if (nearest_enemy == null) return;


        float distance = Vector2.Distance(transform.position, nearest_enemy.transform.position);
        if (distance <= firing_range && !isFiring)
        {
            Vector2 direction = (nearest_enemy.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 85;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            if (0 > angle && angle > -180)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }
            }
            else
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                }
            }
            StartCoroutine(StartShooting());    
        }

    }
}
