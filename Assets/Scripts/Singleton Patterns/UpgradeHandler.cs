using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    //static UpgradeHandler instance;

    List<GameObject> buffs;
    List<GameObject> weapons;
    List<Module> modules;

    ProductionManager productionManager;
    Player player;
    PlayerStats playerStats;
    // THESE LISTS ARE USED FOR INVENTORY TRACKING

    int weaponCount;

    void Awake()
    {
        productionManager = FindAnyObjectByType<ProductionManager>();
        player = FindAnyObjectByType<Player>();
        playerStats = player.GetComponent<PlayerStats>();

        //ManageSingleton();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject weapon in weapons)
        {
            // Weapon leveling logic
            if (player != null)
            {
                Instantiate(weapon, player.transform);
            }
            else
            {
                Instantiate(weapon, transform);
                Debug.Log("Player not found, attaching weapon to UpgradeHandler instead.");
            }
            weaponCount++;
            // Check if weapon is unique
        }

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

        modules = productionManager.GetModuleList();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddWeapon(GameObject prefab)
    {
        weapons.Add(prefab);
    }

    public void AddModule(GameObject prefab)
    {
        productionManager.BuyModule(prefab.GetComponent<Module>().GetName(), 0);
    }

    public void AddBuff(GameObject prefab)
    {
        buffs.Add(prefab);
    }

    public int GetWeaponCount()
    {
        return weaponCount;
    }

    //void ManageSingleton()
    //{
    //    if (instance != null)
    //    {
    //        gameObject.SetActive(false);
    //        Destroy(gameObject);
    //    }

    //    else
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}
}
