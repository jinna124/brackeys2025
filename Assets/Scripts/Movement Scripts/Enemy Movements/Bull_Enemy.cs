using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Bull_Enemy : enemy_movement
{
    [Header("Movement Settings")]
    [Tooltip("Max range at which the enemy can be far away from the player")]
    [SerializeField] float max_range = 8;
    [Tooltip("Minimum range at which the enemy can be close to the player")]
    [SerializeField] float min_range = 5;
    [Space]

    [Header("Charging Settings")]
    [Tooltip("Time in seconds at which the enemy will pause before charging")]
    [SerializeField] float pause_time = 3f;
    [Tooltip("Time in seconds at which how many seconds will the enemy keep charging")]
    [SerializeField] float chargingTime = 1f;
    [Tooltip("Speed of charging")]
    [SerializeField] float charging_speed = 20f;
    [Tooltip("Cooldown before the enemy starts moving again after the charge")]
    [SerializeField] float cooldown = 1f;

    private bool isCharging = false;

    private void FixedUpdate()
    {
        if (!isCharging)
            MoveEnemy();
    }

    protected override void MoveEnemy()
    {
        if (player != null)
        {
            // fetch unit vector in direction of player and distance
            Vector2 direction_to_player = (player.transform.position - transform.position).normalized;
            float distance_to_player = Vector2.Distance(transform.position, player.transform.position);

            // checking
            if (distance_to_player > max_range)
            {
                approach(direction_to_player, distance_to_player);
            }
            else if (distance_to_player < min_range)
            {
                retreat(direction_to_player, distance_to_player);
            }
            else
            {
                rb.linearVelocity = Vector2.zero;   // stop first

                if (!isCharging && distance_to_player <= max_range + 0.5f)  // this allows it to windup whenever the ally gets close 
                {
                    StartCoroutine(WindUpAndCharge());
                }
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
        rb.MovePosition(Vector2.Lerp(rb.position, target_position, 0.5f));
    }

    void approach(Vector2 direction_to_player, float distance_to_player)
    {
        float approach_speed = Mathf.Clamp((distance_to_player - max_range), 0f, 1f) * movement_speed;

        Vector2 target_position = rb.position + direction_to_player * approach_speed * Time.fixedDeltaTime;

        Debug.Log("Enemy is approaching");

        rb.MovePosition(Vector2.Lerp(rb.position, target_position, 0.5f));
    }

    IEnumerator WindUpAndCharge()
    {
        isCharging = true;

        // -----------winding up--------------------
        float windupDistance = 2f;      // distance at which the enemy will windup
        float windupSpeed = 1.5f;       // speed at which the enemy will wind up
        float traveled = 0f;            // a placeholder to track the traveled distances  

        while(traveled < windupDistance)
        {
            Vector2 direction_ = ((Vector2)player.transform.position - rb.position).normalized;
            rb.linearVelocity = -direction_ * windupSpeed;
            traveled += windupSpeed * Time.fixedDeltaTime; // this so that it moves a tiny step backward each loop
            yield return new WaitForFixedUpdate();
        }
        // ------------------------------------------

        // pause first in between
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(pause_time);

        
        // ------------------CHARGE------------------
        float charge_timer = 0f;    // timer to count the number of seconds and keep charging
        Vector2 direction = ((Vector2)player.transform.position - rb.position).normalized;
        while (charge_timer < chargingTime)
        {
            rb.linearVelocity = direction * charging_speed;   // move quickly towards the player

            charge_timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }   
        // ------------------------------------------


        // then pause and retry

        rb.linearVelocity = Vector2.zero;

        // wait for a second before trying again
        yield return new WaitForSeconds(cooldown);

        isCharging = false;
    }
}
