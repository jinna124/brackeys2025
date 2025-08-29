using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

// Player controls
// TODO: Implement shooting mechanics
public class Player : MonoBehaviour
{
    private float moveSpeed = 13f;
    Vector2 rawInput;
    Rigidbody2D rb;
    private PlayerStats playerstats;

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    private void Awake()
    {
        playerstats = GetComponent<PlayerStats>();
        moveSpeed = playerstats.getMovementSpeed();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called once every 0.02 seconds
    void FixedUpdate()
    {
        Move();    
    }
    void Move()
    {
        Vector2 movement = new Vector2(rawInput.x, rawInput.y);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

}

