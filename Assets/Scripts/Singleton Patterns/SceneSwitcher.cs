using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher instance;
    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] GameObject cookiePanel;

    void Awake()
    {
        ManageSingleton();
        upgradeCanvas.SetActive(false);
        cookiePanel.SetActive(true);
    }
    public void LoadGachaScene()
    {
        LoadScene("Gacha");
    }

    public void LoadCombatScene()
    {
        LoadScene("BulletHell");
    }

    public void LoadUpgradesScene()
    {
        upgradeCanvas.SetActive(true);
        cookiePanel.SetActive(false);
    }

    public void LoadManufacturingScene()
    {
        LoadScene("Manufacturing");
    }

    public void LoadInventoryScene()
    {
        LoadScene("Inventory");
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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
