using System.Collections.Generic;
using UnityEngine;

public class ProductionManager : MonoBehaviour
{
    static ProductionManager instance;
    List<Module> moduleList;
    CookieManager cookieManager;
    float timeSinceLastProduction;

    void Awake()
    {
        ManageSingleton();
    }
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
            if (moduleList != null)
            {
                foreach (Module module in moduleList)
                {
                    cookieManager.AddCookies(module.GetCPS());
                }
                timeSinceLastProduction = 0f;

            }
            else
            {
                Debug.Log("ModuleList is Null! Have you added a module yet?");
            }

        }
    }

    public void BuyModule(GameObject prefab)
    {
        CookieManager cookieManager = FindAnyObjectByType<CookieManager>();
        if (cookieManager.GetCookies() >= prefab.GetComponent<Module>().GetPrice())
        {
            cookieManager.SpendCookies(prefab.GetComponent<Module>().GetPrice());
            Debug.Log("Bought " + prefab.GetComponent<Module>().GetName() + " for " + prefab.GetComponent<Module>().GetPrice() + " cookies.");
            moduleList.Add(prefab.GetComponent<Module>());


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
