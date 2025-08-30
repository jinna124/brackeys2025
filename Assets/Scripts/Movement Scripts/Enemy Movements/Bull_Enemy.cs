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
    [SerializeField] Animator animator;

    private bool isCharging = false;
    private bool isWindingUp = false;
    private bool isMoving = false;

    private void Update()
    {
        if (player != null)
        {
            if (!isCharging)
            {
                Vector2 toPlayer = (player.transform.position - transform.position).normalized;
                transform.localScale = new Vector3(Mathf.Sign(toPlayer.x), 1, 1);
            }
            float distance_to_player_ = Vector2.Distance(transform.position, player.transform.position);
            isMoving = !isCharging && !isWindingUp && (distance_to_player_ > max_range);
            animator.SetBool("isMoving", isMoving);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }


    private void FixedUpdate()
    {
        if (!isCharging && !isWindingUp)
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
            else
            {
                rb.linearVelocity = Vector2.zero;   // stop first

                if (!isCharging && distance_to_player <= max_range)  // this allows it to windup whenever the ally gets close 
                {
                    StartCoroutine(WindUpAndCharge());
                }
            }
        }
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
        isWindingUp = true;
        animator.SetBool("isWindingUp", isWindingUp);
        // -----------winding up--------------------
        float windupDistance = 2f;      // distance at which the enemy will windup
        float windupSpeed = 3f;       // speed at which the enemy will wind up
        float traveled = 0f;            // a placeholder to track the traveled distances  

        while(traveled < windupDistance)
        {
            Vector2 direction_ = ((Vector2)player.transform.position - rb.position).normalized;
            transform.localScale = new Vector3(Mathf.Sign(direction_.x), 1, 1);
            rb.linearVelocity = Vector2.zero;
            traveled += windupSpeed * Time.fixedDeltaTime; // this so that it moves a tiny step backward each loop
            yield return new WaitForFixedUpdate();
        }
        // ------------------------------------------

        // pause first in between
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(pause_time);
        isWindingUp = false;
        animator.SetBool("isWindingUp", isWindingUp);
        
        // ------------------CHARGE------------------
        isCharging = true;
        animator.SetBool("isCharging", isCharging);
        animator.SetBool("isMoving", false);
        float charge_timer = 0f;    // timer to count the number of seconds and keep charging
        Vector2 direction = ((Vector2)player.transform.position - rb.position).normalized;
        transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
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
        animator.SetBool("isCharging", isCharging);
    }
}
