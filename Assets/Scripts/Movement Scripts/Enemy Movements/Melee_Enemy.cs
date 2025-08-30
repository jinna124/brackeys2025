using UnityEngine;
using UnityEngine.UIElements;

public class Melee_Enemy : enemy_movement
{
    [Header("Melee Settings")]
    [Tooltip("Range at which the enemy will stop to attack the player")]
    [SerializeField] float melee_range = 0.5f;
    [SerializeField] Animator animator;
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

            float distance_to_player = Vector2.Distance(transform.position, player.transform.position);

            // Set animator bool for running
            bool isRunning = distance_to_player - 1 > melee_range;
            animator.SetBool("isMoving", isRunning);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
    private void FixedUpdate()
    {
        MoveEnemy();
    }
    protected override void MoveEnemy()
    {
        if (player != null)
        {
            float distance_to_player = Vector2.Distance(transform.position, player.transform.position);
            approach(distance_to_player);
        }

    }
    void approach(float distance_to_player)
    {
        if (distance_to_player > melee_range + 1f)
        {
            Debug.Log("Enemy is approaching");
            rb.MovePosition(Vector2.MoveTowards(rb.position, player.transform.position, movement_speed * Time.fixedDeltaTime));

        }
    }
}

