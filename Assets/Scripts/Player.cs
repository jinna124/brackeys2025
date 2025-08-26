using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

// Player controls
// TODO: Implement shooting mechanics
public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Vector2 rawInput;
    Rigidbody2D rb;


    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 movement = new Vector2(rawInput.x, rawInput.y);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

    }

}

