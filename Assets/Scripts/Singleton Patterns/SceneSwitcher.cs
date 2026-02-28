using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher instance;
    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] GameObject cookiePanel;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gachaScreen;
    [SerializeField] GameObject moduleShop;
    [SerializeField] GameObject UIOverlay;
    [SerializeField] TextMeshProUGUI cookieCountText;
    [SerializeField] TextMeshProUGUI roundsSurvivedText;
    CookieManager cookieManager;
    XPManager xpManager;
    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ManageSingleton();
        SceneManager.sceneLoaded += OnSceneLoaded;

        if(upgradeCanvas != null) upgradeCanvas.SetActive(false);
        if (cookiePanel != null) cookiePanel.SetActive(true);
        if (gachaScreen != null) gachaScreen.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (moduleShop != null) moduleShop.SetActive(false);
        if (UIOverlay != null) UIOverlay.SetActive(true);
        
        cookieManager = CookieManager.instance;
        xpManager = XPManager.instance;
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (upgradeCanvas == null) upgradeCanvas = FindInactiveByName("Upgrades");
        if (cookiePanel == null) cookiePanel = FindInactiveByName("CookiePanel");
        if (gachaScreen == null) gachaScreen = FindInactiveByName("GachaScreen");
        if (gameOverScreen == null) gameOverScreen = FindInactiveByName("GameOverScreen");
        if (moduleShop == null) moduleShop = FindInactiveByName("ModuleShop");
        if (UIOverlay == null) UIOverlay = FindInactiveByName("UIOverlay");

        if (cookieCountText == null) cookieCountText = FindInactiveByName("CookiesCountText")?.GetComponent<TextMeshProUGUI>();
        if (roundsSurvivedText == null) roundsSurvivedText = FindInactiveByName("RoundsSurvivedText")?.GetComponent<TextMeshProUGUI>();

        cookieManager = CookieManager.instance;
        xpManager = XPManager.instance;

        RewireButtons();
    }

    void RewireButtons()
    {
        foreach (var button in Resources.FindObjectsOfTypeAll<Button>())
        {
            if (!button.gameObject.scene.isLoaded) continue;

            var onClick = button.onClick;
            for (int i = 0; i < onClick.GetPersistentEventCount(); i++)
            {
                Object target = onClick.GetPersistentTarget(i);
                if (target == null || (target is SceneSwitcher && target != (Object)this))
                {
                    string methodName = onClick.GetPersistentMethodName(i);
                    onClick.SetPersistentListenerState(i, UnityEventCallState.Off);
                    onClick.AddListener(delegate { Invoke(methodName, 0f); });
                }
            }
        }
    }

    GameObject FindInactiveByName(string name)
    {
        foreach (var obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (obj.name == name && obj.scene.isLoaded)
                return obj;
        }
        return null;
    }

    #region Loading Components
    public void LoadGachaScene()
    {
        gachaScreen.SetActive(true);
        cookiePanel.SetActive(false);
        UIOverlay.SetActive(false);
        upgradeCanvas.SetActive(false);
        moduleShop.SetActive(false);
    }

    public void LoadModuleShop()
    {
        gachaScreen.SetActive(false);
        cookiePanel.SetActive(true);
        UIOverlay.SetActive(false);
        upgradeCanvas.SetActive(false);
        moduleShop.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f; // Unpause the game
        cookiePanel.SetActive(true);
        upgradeCanvas.SetActive(false);
        //LoadScene("BulletHell");
        var cardChoices = upgradeCanvas.GetComponentsInChildren<CardChoice>();
        foreach (var cardChoice in cardChoices)
        {
            cardChoice.Start();
        }
        Player player = FindAnyObjectByType<Player>();
        // Comment this out if needed
        player.gameObject.GetComponent<Health>().Fullheal();
        UIOverlay.SetActive(true);
        gachaScreen.SetActive(false);
        moduleShop.SetActive(false);
    }

    public void LoadCombatScene()
    {
        audioSource.Play();
        LoadScene("BulletHell");
    }

    public void LoadUpgradesScene()
    {
        Time.timeScale = 0f; // Pause the game

        upgradeCanvas.SetActive(true);
        cookiePanel.SetActive(false);
        UIOverlay.SetActive(false);
        gachaScreen.SetActive(false);
        moduleShop.SetActive(false);
    }

    public void LoadManufacturingScene()
    {
        LoadScene("Manufacturing");
    }

    public void LoadInventoryScene()
    {
        LoadScene("Inventory");
    }
    public void LoadMainMenuScene()
    {
        audioSource.Play();
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadGameOver()
    {
        gameOverScreen.SetActive(true);
        cookiePanel.SetActive(false);
        cookieCountText.text = "and produced " + CookiePanel.FormatNumber(cookieManager.GetCookies()) + "!";
        roundsSurvivedText.text = "You survived " + xpManager.GetLevel() + " rounds";
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    #endregion
    
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
