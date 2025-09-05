using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{

    Slider healthSlider;
    [SerializeField ]Slider xpSlider;
    TextMeshProUGUI XPText;
    Player player;
    Health playerHealth;
    XPManager XPManager;

    // this is for the time text
    private float elapsed_time = 0f;
    [SerializeField] TMP_Text timeelapsedtxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
        XPText = GetComponentInChildren<TextMeshProUGUI>();
        player = FindAnyObjectByType<Player>();
        playerHealth = player.GetComponent<Health>();
        XPManager = XPManager.instance;
    }

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetMaxHealth();
        xpSlider.maxValue = XPManager.GetXPRequirement();
    }
    // Update is called once per frame
    void Update()
    {
        // in case maxhp or xp req changes during runtime
        healthSlider.maxValue = playerHealth.GetMaxHealth();
        xpSlider.maxValue = XPManager.GetXPRequirement();
        // --------------------------------------------------
        
        // updating sliders
        healthSlider.value = playerHealth.GetHealth();
        xpSlider.value = (XPManager.GetXP());
        XPText.text = XPManager.GetXP().ToString() + "/" + XPManager.GetXPRequirement() + " XP Lvl(" + XPManager.GetLevel() + ")";
        // --------------------------------------------------
        
        Debug.Log("Health:" + playerHealth.GetHealth());
        
        // elapsed time text
        elapsed_time += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsed_time / 60);  
        int hours = Mathf.FloorToInt(minutes / 60);
        int seconds = Mathf.FloorToInt(elapsed_time % 60);  // remainder = seconds
        timeelapsedtxt.text = $"Time Elapsed: {hours:D2}:{minutes:D2}:{seconds:D2}";
    }
}
