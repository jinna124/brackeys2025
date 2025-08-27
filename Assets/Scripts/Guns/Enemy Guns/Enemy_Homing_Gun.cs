using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

// NOT IMPLEMENTED IN SCENE YET!!!

public class Enemy_Homing_Gun : MonoBehaviour
{
    [Header("Enemy Firing Settings")]
    [Space]
    [Tooltip("Prefab of the bullet/projectile")]
    [SerializeField] GameObject projectilePrefab;

    [Space]
    [Header("____Range And Speed____")]
    [Tooltip("The Speed at which the bullet/projectile is fired")]
    [SerializeField] float projectileSpeed = 10f;
    [Tooltip("Time in seconds before the projectile gets destroyed (contributes to the firing range)")]
    [SerializeField] float projectileLifetime = 5f;
    [Tooltip("Speed at which the projectile turns")]
    [SerializeField] float projectileTurnRate = 3f;
    [SerializeField] float homingDuration = 3f;

    [Space]

    [Header("____Firing Rate and Variances____")]
    [Tooltip("firingRate and firingVariance both contribute to the range of the randomness of the bullet " +
        "max = firingrate+firingvar" +
        ", min = firingrate-firingvar")]
    [SerializeField] float firingRate = 1f;
    [SerializeField] float firingVariance = 0.05f;
    [Tooltip("This is the minimum firing rate in seconds aka: the minimum delay before another bullet gets shot")]
    [SerializeField] float minimumFiringTime = 0.1f;

    [HideInInspector] public bool isFiring;
    Player player;

    Coroutine firingCoroutine;

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }

    void Start()
    {
        isFiring = true;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine("FireContinuously");
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            // first insantiate a bullet prefab at the location and rotation of the enemy and fetch the rigidbody of that instance
            GameObject instance = Instantiate(projectilePrefab, transform.position, transform.rotation);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            // exception handling part to check for if there is a rigidbody for that instance
            if (rb != null)
            {
                Vector2 direction = ((Vector2)(player.transform.position) - rb.position).normalized;
                float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
                rb.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                StartCoroutine(HomingBullet(rb, player.transform, projectileSpeed, homingDuration, projectileTurnRate));
            }

            Destroy(instance, projectileLifetime);

            yield return new WaitForSeconds(GetRandomFiringTime());    // AI-specific behavior
        }


    }

    IEnumerator HomingBullet(Rigidbody2D rb, Transform target, float speed, float homingDuration, float turnRate)
    {
        float timer = 0f;

        while(timer < homingDuration)
        {
            Vector2 direction = ((Vector2)(target.position) - rb.position).normalized;
            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            rb.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity.normalized, direction, turnRate * Time.fixedDeltaTime) * speed;
            timer += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();

        }
    }



    /// <summary>
    /// generates a random time interval that the bullet will be shot after
    /// </summary>
    /// <returns>float that represents the firing rate</returns>
    float GetRandomFiringTime()
    {
        float randomFiringTime = Random.Range(firingRate - firingVariance,
                                        firingRate + firingVariance);

        return Mathf.Clamp(randomFiringTime, minimumFiringTime, float.MaxValue);
    }
}
