using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject bounds;
    [SerializeField] GameObject enemy_prefab;
    [SerializeField] LayerMask enemy_layer;
    [SerializeField] float cooldown = 3f;
    [SerializeField] int max_cluster_size = 5;
    [SerializeField] int min_cluster_size = 2;
    [SerializeField] float cluster_radius = 2f;
    [SerializeField] float min_spacing_bet_enemies = 0.5f;
    private Camera Camera;
    private float camera_height;
    private float camera_width;
    private bool isSpawning = false;
    private Vector2 minbounds;
    private Vector2 maxbounds;

    void SpawnOffScreen()
    {
        // get radius of the circle that we will spawn from (large enough to be outside of camera)
        float radius = Mathf.Sqrt(Mathf.Pow(camera_height / 2f, 2) + Mathf.Pow(camera_width / 2f, 2));      // radius = hypotenuse of the width and height
        float angle = Random.Range(0, Mathf.PI * 2f);       // angle in radians
        float added_distance = 4f;            // this is an added distance to ensure enemies wont get inside the edges of the screen
        // get offset of that angle aka (x, y) 
        Vector2 circle_offset = 
            new Vector2(                // to do so (x axis = rcostheta, y axis = rsintheta)
            (radius + added_distance) * Mathf.Cos(angle),
            (radius + added_distance) * Mathf.Sin(angle));            

        // center of circle outside of the camera
        Vector2 cluster_center = (Vector2)Camera.transform.position + circle_offset;

        // generate a random cluster size within range
        int cluster_size = Random.Range(min_cluster_size, max_cluster_size + 1);
        int i = 0;

        while (i < cluster_size)
        {
            Vector2 cluster_offset = Random.insideUnitCircle * cluster_radius;
            Vector2 spawn_position = cluster_center + cluster_offset;
            // -----KEEP IN BOUNDS-------
            spawn_position.x = Mathf.Clamp(spawn_position.x, minbounds.x + 1, maxbounds.x - 1);
            spawn_position.y = Mathf.Clamp(spawn_position.y, minbounds.y + 1, maxbounds.y - 1);
            // ------ENSURE IT DOESNT SPAWN OVER ANOTHER ENEMY-------
            Collider2D hit = Physics2D.OverlapCircle(spawn_position, min_spacing_bet_enemies, enemy_layer);

            if (hit == null)
            {
                Instantiate(enemy_prefab, spawn_position, Quaternion.identity);
                i++;
            }
        }   
    }
    IEnumerator Spawn()
    {
        if (enemy_prefab != null)
        {
            isSpawning = true;
            yield return new WaitForSeconds(cooldown);
            SpawnOffScreen();
            Debug.Log("Enemy spawned");
            isSpawning=false;
        }
    }
    
    
    private void Awake()
    {
        SpriteRenderer sr = bounds.GetComponent<SpriteRenderer>();
        minbounds = sr.bounds.min;
        maxbounds = sr.bounds.max;

        Camera = Camera.main;
        camera_height = Camera.orthographicSize * 2f;
        camera_width = camera_height * Camera.aspect;
    }

    private void Update()
    {
        if (!isSpawning)
        {
            StartCoroutine(Spawn());
        }
    }
}
