using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{

    Slider healthSlider;
    TextMeshProUGUI XPText;
    Player player;
    Health playerHealth;
    XPManager XPManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
        XPText = GetComponentInChildren<TextMeshProUGUI>();
        player = FindAnyObjectByType<Player>();
        playerHealth = player.GetComponent<Health>();
        XPManager = FindAnyObjectByType<XPManager>();
    }

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
    }
    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        XPText.text = XPManager.GetXP().ToString() + " XP";

        Debug.Log("Health:" + playerHealth.GetHealth());
    }
}
