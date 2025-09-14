using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher instance;
    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] GameObject cookiePanel;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gachaScreen;
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
        upgradeCanvas.SetActive(false);
        cookiePanel.SetActive(true);
        gachaScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        cookieManager = CookieManager.instance;
        xpManager = XPManager.instance;
    }
    public void LoadGachaScene()
    {
        gachaScreen.SetActive(true);
        cookiePanel.SetActive(false);
        UIOverlay.SetActive(false);
        upgradeCanvas.SetActive(false);
    }

    private void Update()
    {
        /* for debugging purposes
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadUpgradesScene();
        }*/
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
        cookieCountText.text = "and produced " + CookiePanel.FormatNumber(cookieManager.GetCookies()) + " cookies!";
        roundsSurvivedText.text = "You survived " + xpManager.GetLevel() + " rounds";
    }

    public void LoadScene(string sceneName)
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
