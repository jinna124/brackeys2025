using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class RollingCane : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] GameObject rollingCanePrefab;
    [SerializeField] Transform tipOfWeapon;
    [SerializeField] float cane_speed = 10f;
    [SerializeField] float firing_rate = 2f;
    [SerializeField] float rotating_speed = 360f;

    private Player player;      // to fetch the player movement script
    private Coroutine coroutine;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.currentDirection != Vector2.zero && coroutine == null)
        {
            coroutine = StartCoroutine(StartShooting());
        }
        else if (player.currentDirection == Vector2.zero && coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;           // this acts as a bool 
        }
    }


    private IEnumerator StartShooting()
    {
        while (true)
        {
            shoot();
            yield return new WaitForSeconds(firing_rate);
        }
    }


    private void shoot()
    {
        // get direction
        Vector2 direction = player.lastDirection;

        GameObject rollingCane = Instantiate(rollingCanePrefab, tipOfWeapon.position, Quaternion.identity);
        Rigidbody2D rb = rollingCane.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * cane_speed;
        rb.angularVelocity = rotating_speed;

        Destroy(rollingCane, 5f);
    }
}