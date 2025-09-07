using UnityEngine;

public class SaccarinePerfume : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] float dot_interval = 0.5f;
    private float timer = 0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (timer >= dot_interval)
        {
            Health enemy_health = collision.gameObject.GetComponent<Health>();
            if (enemy_health != null ) { enemy_health.TakeDamage(damage); }
            timer = 0f;
        }
        timer += Time.deltaTime;
    }
}