using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeHandler : MonoBehaviour
{
    public static UpgradeHandler instance;
    List<string> buffs = new List<string>();
    [System.NonSerialized] public List<string> weapons = new List<string>();
    List<Module> modules = new List<Module>();

    ProductionManager productionManager;
    Player player;
    public Player Player => player;     // public autoimplemented property to use in cardchoice
    PlayerStats playerStats;
    // THESE LISTS ARE USED FOR INVENTORY TRACKING
    [SerializeField] GameObject inventoryGO;
    Inventory inventory;
    int weaponCount;

    void Awake()
    {
        ManageSingleton();
       
        if (SceneManager.GetActiveScene().name == "BulletHell")
        {
            player = FindAnyObjectByType<Player>();
            playerStats = player.GetComponent<PlayerStats>();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        productionManager = ProductionManager.instance;
        inventory = inventoryGO.GetComponent<Inventory>();
    }

    public void AddWeapon(string weaponName)
    {
        if (!weapons.Contains(weaponName))
        {
            weapons.Add(weaponName);
            HandleWeapons(weaponName);
            inventory.UpdateInventory();
            Debug.Log("Added Weapon: " + weaponName);
            Debug.Log("EQUIPPED WEAPONS ARE: ");
            foreach (string weapon in weapons) Debug.Log(weapon);
        }
    }

    public void AddModule(GameObject prefab)
    {
        productionManager.BuyModule(prefab, 0);
    }

    public void AddBuff(string buffName)
    {
        buffs.Add(buffName);
        HandleBuffs(buffName);
        Debug.Log("Added buff: " + buffName);
        Debug.Log("Buffs list is now: ");
        foreach(string buff in buffs) Debug.Log(buff);
    }

    public int GetWeaponCount()
    {
        return weaponCount;
    }

    public void HandleWeapons(string weaponName)
    {
            Weapons weaponsscript = player.GetComponent<Weapons>();         // fetches the weapon script from the player
                                                                            // Weapon leveling logic
            if (weaponsscript == null) Debug.LogError("No Weapons script found on Player!");
            if (player != null)
            {
                switch (weaponName)
                {
                    case "Cane":
                        weaponsscript.EnableRollingCane(); break;
                    case "Frying Pan":
                        weaponsscript.EnableFryingPan(); break;
                    case "Mr. Muffin":
                        weaponsscript.EnableMrMuffins(); break;
                    case "Oven (Bomb)":
                        weaponsscript.EnableOven(); break;
                    case "Saccharine Perfume":
                        weaponsscript.EnableSacchirePerfume(); break;
                    default: Debug.Log("Weapon not found"); break;
                }
            }
            else
            {
                Debug.Log("Player not found in UpgradeHandler!");
            }
    }
    public void HandleBuffs(string buff)
    {
        PlayerStats playerstats = player.GetComponent<PlayerStats>();
            switch (buff)
            {
                case "MaxHP":
                    playerstats.setMaxHp(5); break;
                case "MoveSpeed":
                    playerstats.setMovementSpeed(2); break;
                case "GlobalDamage":
                    playerstats.setWeaponDamage(); break;
                default:
                    Debug.Log("buff not defined or still not implemented"); break;
            }
    }
    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
