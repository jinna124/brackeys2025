using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Slider gachaSlider;
    [SerializeField] GameObject HpTextGameobject;
    [SerializeField] GameObject pullCountTextGameobject;
    [SerializeField] int pullHpCost = 20;

    private Health playerHealth;
    private TextMeshProUGUI gachaTxt;
    private TextMeshProUGUI pullCountTxt;

    void Start()
    {
        if (player != null) playerHealth = player.GetComponent<Health>();
        if (HpTextGameobject != null) gachaTxt = HpTextGameobject.GetComponent<TextMeshProUGUI>();
        if (pullCountTextGameobject != null) pullCountTxt = pullCountTextGameobject.GetComponent<TextMeshProUGUI>();

        gachaSlider.onValueChanged.AddListener(SliderJump);
    }
    void Update()
    {
        MaxSliderValue();
        CurrertText();
    }


    void MaxSliderValue()
    {
        if (playerHealth != null)
            gachaSlider.maxValue = playerHealth.GetHealth();
    }
    void CurrertText()
    {
        gachaTxt.text =
            ((int)(gachaSlider.value / pullHpCost) * pullHpCost).ToString()
            + " / "
            + gachaSlider.maxValue.ToString("0")
            + " HP cost";

        pullCountTxt.text = Mathf.Floor(gachaSlider.value / pullHpCost).ToString("0") + " pulls";
    }
    void SliderJump(float currentValue)      
    {
        gachaSlider.value = Mathf.Round(currentValue / pullHpCost) * pullHpCost;
    }
}