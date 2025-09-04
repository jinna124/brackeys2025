using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Melee_Enemy : enemy_movement
{
    [Header("Animation Settings")]
    [SerializeField] Animator animator;
    [Space]
    [Header("Melee Settings")]
    [Tooltip("Range at which the enemy will stop to attack the player")]
    [SerializeField] float melee_range = 0.5f;
    [SerializeField] float melee_damage = 10f;
    [SerializeField] float cooldown = 0.5f;
    private bool isAttacking = false;
    private float nextAttackTime = 0;
    private void Update()       // this is where ill handle facing and animation
    {
        HandleFacingAndAnimation();
    }
    private void FixedUpdate()  // this is where we will handle the movement
    {
        MoveEnemy();
    }
    
    protected override void MoveEnemy()     // overridden method from the enemy_movement parent class
    {
        if (player != null)
        {
            float distance_to_player = Vector2.Distance(transform.position, player.transform.position);
            ApproachAndAttack(distance_to_player);
        }
    }
    
    void ApproachAndAttack(float distance_to_player)
    {
        if (distance_to_player > melee_range)
        {
            Debug.Log("Enemy is approaching");
            rb.MovePosition(Vector2.MoveTowards(rb.position, player.transform.position, movement_speed * Time.fixedDeltaTime));
        }
        //else if (distance_to_player <= melee_range + 1 && !isAttacking && Time.time >= nextAttackTime)        // bugged wanted to melee at range
        //{
        //    isAttacking = true;
        //    // play the animation which will call the dealdmg method when the event is triggered
        //    animator.SetTrigger("TartAttack");
        //    nextAttackTime = Time.time + cooldown;
        //}
    }

    void HandleFacingAndAnimation()
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

    //generic functions that are just used by the animation event (BUGGED)
    void DealDamage()
    {
        Health player_health = player.GetComponent<Health>();

        if (player_health != null && player_health.currentHealth > 0)
        {
            rb.AddForce(Vector2.MoveTowards(rb.position, player.transform.position, 50f));
        }
    }
    void EndAttack()
    {
        Debug.Log("Attack ended");
        isAttacking = false;
    }
}

