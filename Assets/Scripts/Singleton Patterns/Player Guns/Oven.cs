using System.Collections;
using UnityEngine;

public class Oven : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] Transform tipOfWeapon;

    [Header("Pan Settings")]
    [SerializeField] GameObject ovenPrefab;
    [SerializeField] float speedOfOven = 1f;
    [SerializeField] float addedLandingDistance = 0f;
    [SerializeField] float rotationSpeed = 720f;
    [Header("Firing Rate & Range")]
    [SerializeField] float firingRange = 6f;
    [SerializeField] float firingRate = 1f;
    [SerializeField] float maxHeight = 2f;

    [SerializeField] CameraShake cameraShake;

    private PlayerStats playerstats;
    private GameObject nearestEnemy;
    private bool isFiring = false;
    private float fireCooldown;
    private AudioSource audioSource;
    private void Awake()
    {
        playerstats = GetComponent<PlayerStats>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (fireCooldown > 0)
        {
                fireCooldown -= Time.deltaTime;
        }

        if (EnemyManager.instance == null) return;

        nearestEnemy = EnemyManager.instance.GetNearestEnemy(transform.position);
        if (nearestEnemy == null || isFiring) return;

        float distance = Vector2.Distance(transform.position, nearestEnemy.transform.position);
        if (distance <= firingRange && fireCooldown <= 0)
        {
            fireCooldown = firingRate;
            StartCoroutine(throwOven(nearestEnemy));
        }
    }
    IEnumerator throwOven(GameObject nearestEnemy)
    {
        isFiring = true;
        float timer = 0f;
        // Spawn at the tip
        Vector2 spawnPoint = tipOfWeapon.position;
        Vector2 direction = ((Vector2)nearestEnemy.transform.position - spawnPoint).normalized;
        Vector2 travelPoint = (Vector2)nearestEnemy.transform.position - direction * addedLandingDistance;        //(DIRECTION -1F) TWEAK WITH THE 1F TO THROW IN FRONT

        // instantiate oven
        GameObject instance = Instantiate(ovenPrefab, spawnPoint, Quaternion.identity);
        DamageDealer dealer = instance.GetComponent<DamageDealer>();
        dealer.damage = playerstats.getWeaponDamage(dealer.damage);
        // disable collider during travel
        Collider2D col = instance.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Vector2 startPos = spawnPoint;
        Vector2 endPos = travelPoint;
        while (timer < speedOfOven)
        {
            timer += Time.deltaTime;
            float time_multiplier = timer / speedOfOven;           // will be used for smoothing across all lerping 
            // then movement slowly
            Vector2 linear_Movement = Vector2.Lerp(startPos, endPos, time_multiplier);

            // move it vertically too (arc shape)
            //float height = -4f * (t - 0.5f) * (t - 0.5f) + 1f;      // LMAO FOUND THIS FORMULA ONLINE NO IDEA WHERE IT CAME FROM TBH
            float height = 4f * maxHeight * time_multiplier * (1f - time_multiplier);
            linear_Movement.y += height;

            instance.transform.position = linear_Movement;

            // scale for it to look a bit 3d
            float scale = Mathf.Lerp(2f, 2.5f, time_multiplier <= 0.5f ? time_multiplier * 2f : (1 - time_multiplier) * 2f);    // i hate maths (not really)
            instance.transform.localScale = new Vector3(scale, scale, 1f);

            if(direction.x < 0)
            {
                instance.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            }
            else
            {
                instance.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
            }
                
            yield return null;
        }
        if (col != null) col.enabled = true;
        instance.transform.position = travelPoint;
        instance.transform.localScale = new Vector3(2f, 2f, 2f);
        if (audioSource != null) audioSource.Play();
        cameraShake.Shake(10);

        isFiring = false;


        if (instance != null) Destroy(instance, 0.5f);
    }
}
