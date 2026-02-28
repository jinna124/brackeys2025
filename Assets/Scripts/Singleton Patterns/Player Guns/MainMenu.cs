using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button startButton;

    void Awake()
    {
        startButton.onClick.AddListener(LoadCombatSceneForMainMenu);
    }

    private void Update()
    {
        /* for debugging purposes
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadUpgradesScene();
        }*/
    }


    public void LoadCombatSceneForMainMenu()
    {
        SceneManager.LoadScene("BulletHell");
    }


}