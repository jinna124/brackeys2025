using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{

    // pew pew
    // should be versatile for multiple weapon types!!
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] float atkSpeed = 15f;
    [SerializeField] Transform spawnPoint;

    [SerializeField] Vector3 mousePos;
    [SerializeField] Camera cam;
    Rigidbody2D rb;
    Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponentInParent<Player>();
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn Point is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 lookDir = (Vector2)mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (0 > angle && angle > -180)
        {
            if (transform.localScale.x != -1)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            if (transform.localScale.x != 1)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
        }
        //transform.RotateAround (player.transform.position, Vector3.forward, angle);
    }
    void OnFire()
    {
        Shoot();
    }

    void Shoot()
    {
    
    }
}
