using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int maxHp = 100;
    private int currentHp;

    [SerializeField] float movementSpeed = 10f;

    [SerializeField] int weaponDamage = 10;

    public int getMaxHp() => maxHp;
    public float getMovementSpeed() => movementSpeed;
    public int getWeaponDamage() => weaponDamage;
    
    public void setMaxHp(int Hp)
    {
        // update maxHp
        maxHp = Hp;
        // full heal
        currentHp = maxHp;
    }

    public void setMovementSpeed(float MovementSpeed)
    {
        // update movementSpeed
        movementSpeed = MovementSpeed;
    }

    public void setWeaponDamage(int WeaponDamage)
    {
        // update weaponDamage
        weaponDamage = WeaponDamage;
    }
}
