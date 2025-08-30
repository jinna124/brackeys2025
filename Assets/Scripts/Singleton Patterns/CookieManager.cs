using UnityEngine;

// Cookie or currency manager
public class CookieManager : MonoBehaviour
{
    static CookieManager instance;
    [SerializeField] int cookies = 0;
    //static CookieManager instance;

    void Awake()
    {
        ManageSingleton();
    }

    public int GetCookies()
    {
        return cookies;
    }

    public void AddCookies(int amount)
    {
        cookies += amount;
    }

    public void ResetCookies()
    {
        cookies = 0;
    }

    public bool SpendCookies(int amount)
    {
        if (cookies >= amount)
        {
            cookies -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetCPS()
    {
        // Calculate cookies per second based on owned modules
        Module[] modules = FindObjectsByType<Module>(FindObjectsSortMode.None);
        float cps = 0f;
        foreach (var module in modules)
        {
            cps += module.GetCPS();
        }
        return cps;
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
