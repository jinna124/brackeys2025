using UnityEngine;
using System.Collections;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    private GameObject nearest_enemy;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(StartSearching());
    }
    IEnumerator StartSearching()
    {
        while (true)
        {
            nearest_enemy = NearestEnemy();
            yield return new WaitForSeconds(0.1f);
        }
    }

    GameObject NearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;

        GameObject nearest_enemy = enemies[0];
        float distance_to_nearest_enemy = Vector2.Distance(transform.position, enemies[0].transform.position);
        foreach (GameObject enemy in enemies)
        {
            float distance_to_enemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance_to_enemy < distance_to_nearest_enemy)
            {
                distance_to_nearest_enemy = distance_to_enemy;
                nearest_enemy = enemy;
            }
        }

        return nearest_enemy;
    }

    GameObject NearestEnemy(Vector3 fromPosition)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;

        GameObject nearest = enemies[0];
        float minDist = Vector2.Distance(fromPosition, nearest.transform.position);

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(fromPosition, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }

    public GameObject GetNearestEnemy()     // getter for the nearest_enemy
    {
        return nearest_enemy;
    }

    public GameObject GetNearestEnemy(Vector3 fromPosition)     // overloading
    {
        return NearestEnemy(fromPosition);
    }
}
