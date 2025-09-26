using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeHandler : MonoBehaviour
{
    public static UpgradeHandler instance;
    List<GameObject> buffs = new List<GameObject>();
    List<string> weapons = new List<string>();
    List<Module> modules = new List<Module>();

    ProductionManager productionManager;
    Player player;
    public Player Player => player;     // public autoimplemented property to use in cardchoice
    PlayerStats playerStats;
    // THESE LISTS ARE USED FOR INVENTORY TRACKING

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
        
        if (buffs != null)
        {
            foreach (GameObject buff in buffs)
            {
                Buff buffComponent = buff.GetComponent<Buff>();
                Buff.BuffType buffType = buffComponent.GetBuffType;

                if (buffType == Buff.BuffType.MaxHP)
                {
                    playerStats.setMaxHp(5); // ADDS 5 HP
                }
                if (buffType == Buff.BuffType.MoveSpeed)
                {
                    playerStats.setMovementSpeed(5); // ADDS 2 MOVEMENT SPEED
                }
                if (buffType == Buff.BuffType.GlobalDamage)
                {
                    playerStats.setWeaponDamage(10); // ADDS 10 GLOBAL DAMAGE
                }
                else
                {
                    Debug.Log("Invalid buff type, unable to apply buff!");
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        //modules = productionManager.GetModuleList();
        //Debug.Log("module list: " + modules);
        //Debug.Log("buff list: " + buffs);
        //Debug.Log("weapon list: " + weapons);
        HandleWeapons(); // move this somewhere else later
    }

    public void AddWeapon(string weaponName)
    {
        weapons.Add(weaponName);
        Debug.Log("Added Weapon: " + weaponName);
        Debug.Log("Weapons list is now: " + weapons);
    }

    public void AddModule(GameObject prefab)
    {
        productionManager.BuyModule(prefab, 0);
    }

    public void AddBuff(GameObject prefab)
    {
        buffs.Add(prefab);
        Debug.Log("Added buff: " + prefab);
        Debug.Log("Buffs list is now: " + buffs);
    }

    public int GetWeaponCount()
    {
        return weaponCount;
    }

    public void HandleWeapons()
    {
        if (weapons != null && weaponCount < 3)
        {

            foreach (string weapon in weapons)
            {   
                Weapons weaponsscript = player.GetComponent<Weapons>();         // fetches the weapon script from the player
                                                                                // Weapon leveling logic
                if (weaponsscript == null) Debug.LogError("No Weapons script found on Player!");
                if (weaponCount < 3)
                {
                    if (player != null)
                    {
                        switch (weapon)
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
                    weaponCount++;
                // Check if weapon is unique
                }
                
                // Check if weapon is unique
            }
        }
        else
        {
            Debug.Log("No weapons to instantiate or weapon limit reached.");
            Debug.Log("Current weapon count: " + weaponCount);
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
