using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] inventorySlots;
    [SerializeField] private Sprite[] weaponsSprites;
    [SerializeField] private GameObject upgradeHandlerGO;
    UpgradeHandler upgradeHandler;
    private List<string> weapons_list = new List<string>();

    private void Start()
    {
        upgradeHandler = upgradeHandlerGO.GetComponent<UpgradeHandler>();
    }
    public void UpdateInventory()
    {
        foreach (var slot in inventorySlots)
        {
            slot.GetComponent<SpriteRenderer>().sprite = null;
        }
        weapons_list = upgradeHandler.weapons;
        for (int i = 0; i < weapons_list.Count; i++)
        {
            SpriteRenderer weaponSprite = inventorySlots[i].GetComponent<SpriteRenderer>(); 
            if (weaponSprite.sprite == null)
                weaponSprite.sprite = weaponsSprites[GetWeapon(weapons_list[i])];       // gets the weapon sprite at index based on name in list
        }
    }

    public int GetWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "Frying Pan":
                return 0;

            case "Mr. Muffin":
                return 1;

            case "Saccharine Perfume":
                return 2;

            case "Oven (Bomb)":
                return 3;

            case "Cane":
                return 4;

            default:
                Debug.Log("Weapon not found");
                return -1;
        }
    }
}
