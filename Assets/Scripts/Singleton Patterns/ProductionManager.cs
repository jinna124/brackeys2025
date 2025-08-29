using System.Collections.Generic;
using UnityEngine;

public class ProductionManager : MonoBehaviour
{
    List<Module> moduleList;
    CookieManager cookieManager;
    float timeSinceLastProduction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cookieManager = FindAnyObjectByType<CookieManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastProduction += Time.deltaTime;
        
        if (timeSinceLastProduction >= 1f)
        {
            foreach (Module module in moduleList)
            {
                cookieManager.AddCookies(module.GetCPS());
            }
            timeSinceLastProduction = 0f;
        }
    }

    public void BuyModule(string name, int price)
    {
        CookieManager cookieManager = FindAnyObjectByType<CookieManager>();
        if (cookieManager.GetCookies() >= price)
        {
            cookieManager.SpendCookies(price);
            Debug.Log("Bought " + name + " for " + price + " cookies.");


        }
        else
        {
            Debug.Log("Not enough cookies to buy " + name + ".");

        }
    }

    public List<Module> GetModuleList()
    {
        return moduleList;
    }
}
