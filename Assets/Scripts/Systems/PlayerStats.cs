using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int maxHp = 100;
    private int currentHp;

    [SerializeField] float movementSpeed = 10f;

    [SerializeField] int weaponDamage = 10;

    private Health healthComponent;
    private Player movementComponent;
    private GameObject[] playerBullets;
    private DamageDealer damageDealer;
    private Animator animator;
    public int getMaxHp() => maxHp;
    public float getMovementSpeed() => movementSpeed;
    public int getWeaponDamage() => weaponDamage;
    
    public void setMaxHp(int Hp)
    {
        // update maxHp
        healthComponent.addMaxHp(Hp);
        healthComponent.Fullheal();
    }

    public void setMovementSpeed(float MovementSpeed)
    {
        // update movementSpeed
        movementComponent.addMovSpd(MovementSpeed);
        animator.speed += 0.2f;
    }

    public void setWeaponDamage(int WeaponDamage)
    {
        // update weaponDamage
        foreach(GameObject bullet in playerBullets)
        {
            damageDealer = bullet.GetComponent<DamageDealer>();
            damageDealer.damage += weaponDamage;
        } 
    }

    private void Awake()
    {
        healthComponent = GetComponent<Health>();
        movementComponent = GetComponent<Player>();
        animator = GetComponent<Animator>();
        playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
    }
}
