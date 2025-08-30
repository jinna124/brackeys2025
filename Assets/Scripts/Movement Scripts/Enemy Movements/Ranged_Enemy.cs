using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Ranged_Enemy : enemy_movement
{
    [Header("Movement Settings")]
    [Tooltip("Max range at which the enemy can be far away from the player")]
    [SerializeField] float max_range = 8;
    [Tooltip("Minimum range at which the enemy can be close to the player")]
    [SerializeField] float min_range = 5;
    [Space]
    [Tooltip("Speed at which the enemy will strafe if within range")]
    [SerializeField] float strafing_speed = 10;
    [Tooltip("Time interval in seconds when the player will generate another strafing direction")]
    [SerializeField] float nextStrafeInterval = 3f;

    private float timer = 0f;
    private float strafing_direction;       // 1 = clockwise, -1 = anticlock

    private void Update()
    {
        if (player != null)
        {
            // Flip sprite horizontally based on player position
            if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }


    private void Start()
    {
        strafing_direction = 1 - 2 * Random.Range(0, 2);  // clockwise or anti-clockwise
    }
    private void FixedUpdate()
    {
        MoveEnemy();
    }

    protected override void MoveEnemy()
    {
        if(player != null)
        {
            // fetch unit vector in direction of player and distance
            Vector2 direction_to_player = (player.transform.position - transform.position).normalized; 
            float distance_to_player = Vector2.Distance(transform.position, player.transform.position);

            // checking
            if(distance_to_player > max_range)
            {
                approach(direction_to_player, distance_to_player);
            }
            else if(distance_to_player < min_range)
            {
                retreat(direction_to_player, distance_to_player);
            }
            else
            {
                strafe(direction_to_player);
            }
        }
    }

    void retreat(Vector2 direction_to_player, float distance_to_player)
    {
        // calculate a smoothing speed
        // this decreases the speed when it is slightly outside of the max range
        // calculates distance between player - max range if it is bet 0, 1 ---> keep it there *movspd
        
        float retreat_speed = Mathf.Clamp((min_range - distance_to_player), 0f, 1f) * movement_speed;


        // calculate where to go exactly (unit vector * how many units to move this frame)
        Vector2 target_position = rb.position - direction_to_player * retreat_speed * Time.fixedDeltaTime;

        Debug.Log("Enemy is retreating");

        // start moving to that vector smoothly
        rb.MovePosition(Vector2.Lerp(rb.position, target_position, 0.3f));
    }

    void approach(Vector2 direction_to_player, float distance_to_player)
    {
        float approach_speed = Mathf.Clamp((distance_to_player - max_range), 0f, 1f) * movement_speed;

        Vector2 target_position = rb.position + direction_to_player * approach_speed * Time.fixedDeltaTime;

        Debug.Log("Enemy is approaching");

        rb.MovePosition(Vector2.Lerp(rb.position, target_position, 0.3f));
    }

    void strafe(Vector2 direction_to_player)
    {
        if(timer >= nextStrafeInterval)       // if time is reached generate another random direction and reset timer
        {
            strafing_direction = 1 - 2 * Random.Range(0, 2);
            timer = 0;
        }

        Vector2 direction = new Vector2(direction_to_player.y, -direction_to_player.x);     // by default anti clock

        Vector2 target_position = rb.position + strafing_direction * direction * strafing_speed * Time.fixedDeltaTime;

        rb.MovePosition(Vector2.Lerp(rb.position,target_position, 0.3f));
        timer += Time.fixedDeltaTime;
    }
}
