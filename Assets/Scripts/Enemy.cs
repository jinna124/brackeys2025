using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

// NOT IMPLEMENTED IN SCENE YET!!!

public class Enemy : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;

    [Header("Enemy")]
    [SerializeField] float firingVariance = 0.05f;
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
            GameObject instance = Instantiate(projectilePrefab, transform.position, transform.rotation);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                Debug.Log(direction);
                rb.linearVelocity = direction * projectileSpeed;

                // THIS IS BUGGED!!!
                // Enemy should be shooting towards the player.
            }
            else
            {
                Debug.Log("uhhh so rb is null");
            }
            Destroy(instance, projectileLifetime);

            yield return new WaitForSeconds(GetRandomFiringTime());    // AI-specific behavior
        }


    }

    float GetRandomFiringTime()
    {
        float randomFiringTime = Random.Range(firingRate - firingVariance,
                                        firingRate + firingVariance);
        return Mathf.Clamp(randomFiringTime, minimumFiringTime, float.MaxValue);
    }
}
