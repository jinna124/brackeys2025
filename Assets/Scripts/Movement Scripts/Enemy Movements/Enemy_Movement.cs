using System.Collections;
using UnityEngine;

public abstract class enemy_movement : MonoBehaviour
{
    [Tooltip("The overall movement speed")]
    [SerializeField] protected float movement_speed = 10f;
    protected Player player;
    protected Rigidbody2D rb;

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected abstract void MoveEnemy();
}