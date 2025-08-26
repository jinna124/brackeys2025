using System.Collections;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    Player player;
    [SerializeField] float movement_speed = 5f;
    [SerializeField] float strafing_speed = 0.5f;
    [SerializeField] float min_range = 8;
    [SerializeField] float max_range = 5;
    [SerializeField] bool isBull = false;
    [SerializeField] float pause_time = 3f;
    [SerializeField] float chargingTime = 1f;
    [SerializeField] float charging_speed = 20f;

    private bool isCharging = false;
    private Rigidbody2D rb;

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isCharging)
        {
            MoveEnemy();
        }
    }

    void MoveEnemy()
    {
        if (player == null)
        {
            Debug.LogError("Player is null!");
            return;
        }

        Vector2 direction_to_player = (player.transform.position - transform.position).normalized;
        float distance_to_player = Vector2.Distance(transform.position, player.transform.position);

        Debug.Log($"Distance: {distance_to_player}, Min: {min_range}, Max: {max_range}, isBull: {isBull}, isCharging: {isCharging}");

        float approach_speed = Mathf.Clamp((distance_to_player - max_range), 0f, 1f) * movement_speed;
        float retreat_speed = Mathf.Clamp((min_range - distance_to_player), 0f, 1f) * movement_speed;

        if (distance_to_player > max_range)         // approaching player
        {
            Debug.Log("Approaching player");
            Vector2 targetpos = rb.position + direction_to_player * approach_speed * Time.fixedDeltaTime;
            rb.MovePosition(Vector2.Lerp(rb.position, targetpos, 0.5f));
        }
        else if (distance_to_player < min_range)   // retreating from player
        {
            Debug.Log("Retreating from player");
            Vector2 targetpos = rb.position - direction_to_player * retreat_speed * Time.fixedDeltaTime;
            rb.MovePosition(Vector2.Lerp(rb.position, targetpos, 0.5f));
        }
        else                                        // in perfect range
        {
            Debug.Log("In perfect range - should charge if bull");
            // Stop moving
            rb.linearVelocity = Vector2.zero;

            if (isBull && !isCharging)
            {
                Debug.Log("Starting charge coroutine!");
                StartCoroutine(Charge());
            }
        }
    }

    IEnumerator Charge()
    {
        Debug.Log("Charge coroutine started!");
        isCharging = true;

        // Pause before charging
        yield return new WaitForSeconds(pause_time);

        Debug.Log("About to charge!");

        // Calculate direction at the moment of charge
        Vector2 chargeDirection = (player.transform.position - transform.position).normalized;

        // IGNORE RANGE CHECKING DURING CHARGE BY USING A WHILE LOOP
        float chargeTimer = 0f;
        while (chargeTimer < chargingTime && player != null)
        {
            // Move directly toward player, ignoring range restrictions
            rb.linearVelocity = chargeDirection * charging_speed;
            chargeTimer += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Charge completed!");

        // Stop the bull
        rb.linearVelocity = Vector2.zero;

        // Optional cooldown
        yield return new WaitForSeconds(0.5f);

        isCharging = false;
        Debug.Log("Ready to charge again");
    }
}
///////////////STRAFING CODE BUGGED
//else
//{
//    Vector2 strafe_clockwise = new Vector2(direction_to_player.y, -direction_to_player.x);
//    rb.linearVelocity = strafe_clockwise * strafe_direction * strafing_speed;

//    if (Time.time >= Timer)
//    {
//        strafe_direction = 1 - 2 * Random.Range(0, 2); // if it is 1 then 1*2 = 2, 1-2 = -1, if it is = 0 then returns 1
//        seconds_to_count = Random.Range(2, 6);
//        Timer = Time.time + seconds_to_count;
//    }
//}