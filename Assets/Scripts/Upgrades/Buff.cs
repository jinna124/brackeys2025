using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField]
    public enum BuffType
    {
        MaxHP,
        MoveSpeed,
        GlobalDamage
    }

    [SerializeField]
    private BuffType buffType;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public string GetName()
    {
        // Return name based on module type
        switch (buffType)
        {
            case BuffType.MaxHP:
                return "Anti-Aging Cream";
            case BuffType.MoveSpeed:
                return "Stroller";
            case BuffType.GlobalDamage:
                return "Pilates Routine";
            default:
                return "Not found";

        }
    }
    public string GetDescription()
    {
        // Return Description based on module type
        switch (buffType)
        {
            case BuffType.MaxHP:
                return "Better skincare routine, better health. Increases Max HP by 5 points.";
            case BuffType.MoveSpeed:
                return "A stroller to get around faster. Increases movement speed by 2.";
            case BuffType.GlobalDamage:
                return "Grandma has discovered Pilates. Increases all damage by 10%.";
            default:
                return "Not found.";
        }
    }
    
    public BuffType GetBuffType { get { return buffType; } }
}
