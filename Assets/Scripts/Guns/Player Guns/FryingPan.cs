using System.Collections;
using UnityEngine;

public class FryingPan : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] Transform tipOfWeapon;

    [Header("Pan Settings")]
    [SerializeField] GameObject panPrefab;
    [SerializeField] float panSpeed = 5f;
    [SerializeField] float spinSpeed = 360f; // degrees per second

    [Header("Firing Rate & Range")]
    [SerializeField] float firingRange = 6f;
    [SerializeField] float firingRate = 1f;

    private GameObject nearestEnemy;
    private bool isFiring = false;

    private enum PanState { NotShot, Going, Returning }
    private PanState state = PanState.NotShot;

    private void Start()
    {
        StartCoroutine(SearchNearestEnemy());
    }

    private void Update()
    {
        if (nearestEnemy == null || isFiring) return;

        float distance = Vector2.Distance(transform.position, nearestEnemy.transform.position);
        if (distance <= firingRange)
        {
            StartCoroutine(ShootPan(nearestEnemy));
        }
    }

    private IEnumerator SearchNearestEnemy()
    {
        while (true)
        {
            nearestEnemy = FindNearestEnemy();
            yield return new WaitForSeconds(0.1f); // small interval to avoid expensive calls every frame
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;

        GameObject nearest = enemies[0];
        float minDist = Vector2.Distance(transform.position, nearest.transform.position);

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }

    private IEnumerator ShootPan(GameObject target)
    {
        isFiring = true;
        state = PanState.Going;

        // Spawn at the tip
        Vector2 spawnPoint = tipOfWeapon.position;
        Vector2 direction = ((Vector2)target.transform.position - spawnPoint).normalized;
        Vector2 travelPoint = (Vector2)target.transform.position + direction * firingRange;

        // Instantiate pan
        GameObject panInstance = Instantiate(panPrefab, spawnPoint, Quaternion.identity);

        // Going out
        while (Vector2.Distance(panInstance.transform.position, travelPoint) > 0.1f)
        {
            panInstance.transform.position = Vector2.MoveTowards(panInstance.transform.position, travelPoint, panSpeed * Time.deltaTime);
            panInstance.transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
            yield return null;
        }

        // Returning
        state = PanState.Returning;
        while (Vector2.Distance(panInstance.transform.position, tipOfWeapon.position) > 0.1f)
        {
            // Always move toward the current tip position
            panInstance.transform.position = Vector2.MoveTowards(panInstance.transform.position, tipOfWeapon.position, panSpeed * Time.deltaTime);
            panInstance.transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
            yield return null;
        }

        // Snap to tip and destroy
        panInstance.transform.position = tipOfWeapon.position;
        Destroy(panInstance);

        state = PanState.NotShot;
        isFiring = false;
    }

}
