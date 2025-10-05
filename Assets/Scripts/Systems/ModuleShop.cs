using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ModuleShop : MonoBehaviour
{
    // Class used for individual module shop items
    [SerializeField] TextMeshProUGUI moduleTotalCPSText;
    [SerializeField] TextMeshProUGUI moduleCPSText;
    [SerializeField] TextMeshProUGUI moduleCostText;
    [SerializeField] TextMeshProUGUI moduleCountText;
    [SerializeField] Button buyButton;
    [SerializeField] GameObject modulePrefab;
    [SerializeField] float priceMultiplier = 1.15f;
    Module module;
    int price; 
    int count;
    CookieManager cookieManager;
    ProductionManager productionManager;

    void Start()
    {
        cookieManager = CookieManager.instance;
        productionManager = ProductionManager.instance;

        count = productionManager.GetModuleCount(modulePrefab);
        module = modulePrefab.GetComponent<Module>();
        price = module.GetPrice();
        UpdateUI();
        buyButton.onClick.AddListener(() =>
        {
            productionManager.BuyModule(modulePrefab, price);
            UpdateUI();
            price = (int)(price * priceMultiplier);
        });
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        count = productionManager.GetModuleCount(modulePrefab);
        if (cookieManager.GetCookies() >= price)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }

        if (module == null)
        {
            Debug.LogError("Module is null! Did you assign the module prefab in the inspector?");
            return;
        }

        if (count < 1)
        {
            moduleCPSText.text = "Not Owned";
            moduleTotalCPSText.text = module.GetDescription();
        }
        else
        {
            moduleCPSText.text = "CPS: " + module.GetCPS();
            moduleTotalCPSText.text = "Total CPS: " + (module.GetCPS() * productionManager.GetModuleCount(modulePrefab)).ToString("F1");
        }
        moduleCountText.text = productionManager.GetModuleCount(modulePrefab).ToString();
        moduleCostText.text = price.ToString();
        
    }
}

