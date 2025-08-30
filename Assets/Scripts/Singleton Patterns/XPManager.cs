using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class XPManager : MonoBehaviour
{
    static XPManager instance;
    // Singleton pattern (probably bad practice, but eh)
    [SerializeField] XPShard XPShardPrefab;
    int XP = 0;
    int XPRequirement;
    int baseXPRequirement = 30;
    int level = 0;
    int[] XPPerLevel = { 0, 10, 20, 20, 30, 40, 40, 50 }; // XP required for each level
    SceneSwitcher sceneSwitcher;

    void Awake()
    {
        ManageSingleton();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        XPRequirement = baseXPRequirement;
        sceneSwitcher = FindAnyObjectByType<SceneSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current XP: " + XP + "/" + XPRequirement);
        if (XP >= XPRequirement)
        {
            LevelUp();
        }
    }

    public int GetXP() { return XP; }
    public void AddXP(int value) { XP += value; }
    public void ResetXP() { XP = 0; }
    public XPShard GetXPShardPrefab() { return XPShardPrefab; }

    void LevelUp()
    {
        XPRequirement = baseXPRequirement + XPPerLevel[level];
        Debug.Log("Level Up! New XP Requirement: " + XPRequirement);
        level++;
        sceneSwitcher.LoadUpgradesScene();
        ResetXP();

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

    //void ManageSingleton()
    //{
    //    if (instance != null)
    //    {
    //        gameObject.SetActive(false);
    //        Destroy(gameObject);
    //    }

    //    else
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}
}
