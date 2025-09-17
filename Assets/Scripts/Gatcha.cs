using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gatcha : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Slider gachaslider;
    private TextMeshProUGUI gachatxt;
    [SerializeField] GameObject gachatxtgameobject;
    private TextMeshProUGUI pullcounttxt;
    [SerializeField] GameObject pullcounttxtgameobject;

    private Health playerHealth;

    void Start()
    {
        if (player != null)
            playerHealth = player.GetComponent<Health>();
        if (gachatxtgameobject != null)
            gachatxt = gachatxtgameobject.GetComponent<TextMeshProUGUI>();
        if (pullcounttxtgameobject != null)
        {
            pullcounttxt = pullcounttxtgameobject.GetComponent<TextMeshProUGUI>();
        }
    }
    void Update()
    {
        if (playerHealth != null)
        {
            MaxSliderValue();
            CurrertText();
        }
    }
    void MaxSliderValue()
    {
        if (playerHealth != null)
            gachaslider.maxValue = playerHealth.GetHealth();
    }
    void CurrertText()
    {
        gachatxt.text = multiply(((gachaslider.value) / 20), 20).ToString() + " / " + gachaslider.maxValue.ToString("0") + " HP cost";
        pullcounttxt.text = Mathf.Floor(((gachaslider.value) / 20)).ToString("0") + " pulls";
    }
    int multiply(float a, int b)
    {
        int c = (int)a;
        return c * b;
    }
}