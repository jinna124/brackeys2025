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
    private Animator animator;

    public Vector2 currentDirection => rawInput;    
    public Vector2 lastDirection { get; set; } = Vector2.zero;      // autoimplemented property

    private bool isMoving = false;
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();

        if(rawInput != Vector2.zero)        // if he is moving update the lastdirection (shootingdirection)
        {
            lastDirection = rawInput.normalized;
        }
    }

    private void Awake()
    {
        playerstats = GetComponent<PlayerStats>();
        moveSpeed = playerstats.getMovementSpeed();
        animator = GetComponent<Animator>();
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
        
        if(rawInput.x != 0)
        {
            transform.localScale = new Vector3(-Mathf.Sign(rawInput.x), 1, 1);
        }

        // set walking to be true if it is moving
        bool isWalking = rawInput.sqrMagnitude > 0;
        animator.SetBool("isWalking", isWalking);       // cause x!= 0 works only on horizontal movement
    }
    void Move()
    {
        Vector2 movement = new Vector2(rawInput.x, rawInput.y);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    public void addMovSpd(float addedvalue)
    {
        moveSpeed += addedvalue;
    }
}

