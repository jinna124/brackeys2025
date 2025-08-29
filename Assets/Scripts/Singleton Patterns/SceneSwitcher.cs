using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadGachaScene()
    {
        LoadScene("Gacha");
    }

    public void LoadCombatScene()
    {
        LoadScene("BulletHell");
    }

    public void LoadUpgradesScene()
    {
        LoadScene("Upgrades");
    }

    public void LoadManufacturingScene()
    {
        LoadScene("Manufacturing");
    }

    public void LoadInventoryScene()
    {
        LoadScene("Inventory");
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
