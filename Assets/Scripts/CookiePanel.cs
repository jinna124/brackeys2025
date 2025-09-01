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
        PlayParticleEffect();
    }

    // Update is called once per frame
    void Update()
    {
        cookieText.text = FormatNumber(cookieManager.GetCookies());
        cpsText.text = FormatNumber(cookieManager.GetCPS()) + " CPS";
    }

    void PlayParticleEffect()
    {
        cookieParticles.Play();
        var emission = cookieParticles.emission;
        if (cookieManager.GetCPS() > 0)
        {
            emission.rateOverTime = 10;
        }
        else
        {
            emission.rateOverTime = 0;
        }
    }

    [SerializeField] private static readonly string[] suffixes =
    {
        "", "thousand", "million", "billion", "trillion", "quadrillion",
        "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion"
    };

    public static string FormatNumber(float number)
    {
        if (number == 0)
            return "0";

        int magnitude = (int)Math.Floor(Math.Log10(number) / 3);
        magnitude = Math.Min(magnitude, suffixes.Length - 1);

        float shortNumber = (float)(number / Math.Pow(1000, magnitude));

        return shortNumber.ToString("0.##") + " " + suffixes[magnitude];
    }
}
