using System.Collections;
using UnityEngine;
public class Oven : Weapons
{
    [Header("Weapon Settings")]
    [SerializeField] Transform tipOfWeapon;

    [Header("Pan Settings")]
    [SerializeField] GameObject ovenPrefab;

    [Header("Firing Rate & Range")]
    [SerializeField] float firingRange = 6f;
    [SerializeField] float firingRate = 1f;
    [SerializeField] float maxHeight = 2f;
    [Tooltip("Time to reach maximum height")]
    [SerializeField] float duration = 1f;

    private GameObject nearestEnemy;
    private bool isFiring = false;

    private void Update()
    {
        nearestEnemy = EnemyManager.instance.GetNearestEnemy(transform.position);
        if (nearestEnemy == null || isFiring) return;

        float distance = Vector2.Distance(transform.position, nearestEnemy.transform.position);
        if (distance <= firingRange)
        {
            StartCoroutine(throwOven(nearestEnemy));
        }
    }
    IEnumerator throwOven(GameObject nearestEnemy)
    {
        isFiring = true;
        float timer = 0f;
        float fullduration = 2f;
        // Spawn at the tip
        Vector2 spawnPoint = tipOfWeapon.position;
        Vector2 direction = ((Vector2)nearestEnemy.transform.position - spawnPoint).normalized;
        Vector2 travelPoint = (Vector2)nearestEnemy.transform.position - direction * 0.5f;        //(DIRECTION -1F) TWEAK WITH THE 1F TO THROW IN FRONT

        // instantiate oven
        GameObject instance = Instantiate(ovenPrefab, spawnPoint, Quaternion.identity);
        // disable collider during travel
        Collider2D col = instance.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Vector2 startPos = spawnPoint;
        Vector2 endPos = travelPoint;
        while (timer < fullduration)
        {
            timer += Time.deltaTime;
            float time_multiplier = timer / fullduration;           // will be used for smoothing across all lerping 
            // then movement slowly
            Vector2 linear_Movement = Vector2.Lerp(startPos, endPos, time_multiplier);

            // move it vertically too (arc shape)
            //float height = -4f * (t - 0.5f) * (t - 0.5f) + 1f;      // LMAO FOUND THIS FORMULA ONLINE NO IDEA WHERE IT CAME FROM TBH
            float height = 4f * maxHeight * time_multiplier * (1f - time_multiplier);
            linear_Movement.y += height;

            instance.transform.position = linear_Movement;

            // scale for it to look a bit 3d
            float scale = Mathf.Lerp(1f, 1.5f, time_multiplier <= 0.5f ? time_multiplier * 2f : (1 - time_multiplier) * 2f);    // i hate maths (not really)
            instance.transform.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }
        if (col != null) col.enabled = true;

        instance.transform.position = travelPoint;
        instance.transform.localScale = new Vector3(1f, 1f, 1f);

        isFiring = false;

        if (instance != null) Destroy(instance, 0.5f);
    }
}
