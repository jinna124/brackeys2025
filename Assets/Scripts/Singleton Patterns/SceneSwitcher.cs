using UnityEditor.SearchService;
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

    private void Update()
    {
        /* for debugging purposes
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadUpgradesScene();
        }*/
    }

    public void LoadUpgradeScene()
    {
        Time.timeScale = 1f; // Pause the game
        cookiePanel.SetActive(true);
        upgradeCanvas.SetActive(false);
        //LoadScene("BulletHell");
    }

    public void LoadCombatScene()
    {
        LoadScene("BulletHell");
    }

    public void LoadUpgradesScene()
    {
        Time.timeScale = 0f; // Pause the game
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
