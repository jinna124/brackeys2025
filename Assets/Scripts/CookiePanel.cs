using UnityEngine;
using TMPro;
using System;


public class CookiePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cookieText;
    [SerializeField] TextMeshProUGUI cpsText;
    CookieManager cookieManager;
    ParticleSystem cookieParticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cookieManager = CookieManager.instance;
        cookieParticles = GetComponentInChildren<ParticleSystem>();
        cookieParticles.Play();
        ScaleEmissionRate();
    }

    // Update is called once per frame
    void Update()
    {
        cookieText.text = FormatNumber(cookieManager.GetCookies());
        cpsText.text = "CPS: " + FormatNumber(cookieManager.GetCPS());
        ScaleEmissionRate();
        Debug.Log(cookieManager.GetCPS());
    }

    void ScaleEmissionRate()
    {
        var emission = cookieParticles.emission;
        float cps = cookieManager.GetCPS();

        if (cps <= 0f)
        {
            emission.rateOverTime = 0f;
            return;
        }

        // Simple linear scaling: tweak scale to taste
        const float minRate = 5f;
        const float maxRate = 60f;
        const float scale = 0.02f; // emission particles per CPS

        float rate = minRate + cps * scale;
        rate = Mathf.Clamp(rate, minRate, maxRate);
        emission.rateOverTime = rate;
    }

    [SerializeField] private static readonly string[] suffixes =
    {
        "", "thousand", "million", "billion", "trillion", "quadrillion",
        "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion"
    };

    public static string FormatNumber(float number)
    {
        if (number == 0)
            return "0 cookies";
        
        if (number == 1)
            return "1 cookie";

        int magnitude = (int)Math.Floor(Math.Log10(number) / 3);
        magnitude = Math.Min(magnitude, suffixes.Length - 1);

        float shortNumber = (float)(number / Math.Pow(1000, magnitude));

        return shortNumber.ToString("0.##") + " " + suffixes[magnitude] + " cookies";
    }
}
