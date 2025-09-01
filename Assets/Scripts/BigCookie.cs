using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BigCookie : MonoBehaviour
{

    CookieManager cookieManager;
    Button self;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cookieManager = CookieManager.instance;
        self = GetComponent<Button>();
        self.onClick.AddListener(OnClickBigCookie);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnClickBigCookie()
    {

        if (ProductionManager.instance.GetModuleList() != null)
        {
            cookieManager.AddCookies(Mathf.Max(1, (int)(cookieManager.GetCPS() * 0.1)));
        }
        else
        {
            cookieManager.AddCookies(1);
        }
    }
}
