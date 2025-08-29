using UnityEngine;

public class Module : MonoBehaviour
{
    [SerializeField]
    enum ModuleType
    {
        Rolling_Pin,
        Hand_Mixer,
        Industrial_Mixer,
        Boiler,
        Bakery_Factory
    }

    [SerializeField]
    private ModuleType moduleType;

    public int GetCPS()
    {
        // Return CPS based on module type
        switch (moduleType)
        {
            case ModuleType.Rolling_Pin:
                return 1;
            case ModuleType.Hand_Mixer:
                return 5;
            case ModuleType.Industrial_Mixer:
                return 10;
            case ModuleType.Boiler:
                return 25;
            case ModuleType.Bakery_Factory:
                return 50;
            default:
                return 1;
        }
    }

    public string GetName()
    {
        // Return name based on module type
        switch (moduleType)
        {
            case ModuleType.Rolling_Pin:
                return "Rolling Pin";
            case ModuleType.Hand_Mixer:
                return "Hand Mixer";
            case ModuleType.Industrial_Mixer:
                return "Industrial Mixer";
            case ModuleType.Boiler:
                return "Boiler";
            case ModuleType.Bakery_Factory:
                return "Bakery Factory";
            default:
                return "Not found";

        }
    }
    public string GetDescription()
    {
        // Return Description based on module type
        switch (moduleType)
        {
            case ModuleType.Rolling_Pin:
                return "Classic tool of the trade. Slowly flattens dough into cookies - the nonna way.";
            case ModuleType.Hand_Mixer:
                return "Buzz buzz! A handy little helper that whips up cookies faster than hand-rolling, of course.";
            case ModuleType.Industrial_Mixer:
                return "A true monster of dough mixing. Produces cookies at industrial strength. Technologia!";
            case ModuleType.Boiler:
                return "Simmering syrup and caramel - Syrup this hot feels almost like lava.";
            case ModuleType.Bakery_Factory:
                return "Nonna’s masterpiece rebuilt - cranks out cookies at full power for the Ultimate Cookie. ";
            default:
                return "Not found";
        }
    }
    
    public int GetPrice()
    {
        // Return Price based on module type
        switch (moduleType)
        {
            case ModuleType.Rolling_Pin:
                return 15;
            case ModuleType.Hand_Mixer:
                return 80;
            case ModuleType.Industrial_Mixer:
                return 450;
            case ModuleType.Boiler:
                return 2600;
            case ModuleType.Bakery_Factory:
                return 14000;
            default:
                return 15;
        }
    }
}
