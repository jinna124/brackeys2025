using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Gatcha : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Slider gachaSlider;
    [SerializeField] GameObject gachaTxtGameobject;
    [SerializeField] GameObject pullCountTxtGameobject;

    private Health playerHealth;
    private TextMeshProUGUI gachaTxt;
    private TextMeshProUGUI pullCountTxt;
    private float previousSliderValue;

    void Start()
    {
        if (player != null)
            playerHealth = player.GetComponent<Health>();
        if (gachaTxtGameobject != null)
            gachaTxt = gachaTxtGameobject.GetComponent<TextMeshProUGUI>();
        if (pullCountTxtGameobject != null)
            pullCountTxt = pullCountTxtGameobject.GetComponent<TextMeshProUGUI>();

        previousSliderValue = gachaSlider.value;
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
            ((int)(gachaSlider.value / 20) * 20).ToString()
            + " / "
            + gachaSlider.maxValue.ToString("0")
            + " HP cost";

        pullCountTxt.text = Mathf.Floor(gachaSlider.value / 20).ToString("0") + " pulls";
    }

    void SliderJump(float currentValue)      
    {
        float stepSize = 20;

        //if (currentValue + previousSliderValue >= 2)
        //{
        //    gachaSlider.value = previousSliderValue + stepSize;
        //}
        //else if(Mathf.Abs(currentValue - previousSliderValue) >= 2f) 
        //{
        //    gachaSlider.value = previousSliderValue - stepSize;
        //}

        //gachaSlider.value = Mathf.Clamp(gachaSlider.value, gachaSlider.minValue, gachaSlider.maxValue);

        //previousSliderValue = gachaSlider.value;

        gachaSlider.value = Mathf.Round(currentValue / stepSize) * stepSize;
    }
}