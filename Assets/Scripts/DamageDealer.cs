using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    // Doesn't do anything, is just there to mark the object as a damage dealer and enable access from other scripts
    [SerializeField] float damage = 10f;

    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
