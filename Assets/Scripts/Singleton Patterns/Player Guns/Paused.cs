using UnityEngine;

public class PausedAndInventory : MonoBehaviour
{
    [SerializeField] GameObject pausetextobject;
    [SerializeField] GameObject inventorySystem;
    private bool isPaused = false;
    private AudioSource pauseaudiosource;

    void Awake()
    {
        pauseaudiosource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        checkforinput();
    }

    void checkforinput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseaudiosource.Play();
            if (!isPaused)
            {
                pausetextobject.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
            }
            else
            {
                pausetextobject.SetActive(false);
                isPaused = false;
                Time.timeScale = 1f;
            }
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            inventorySystem.SetActive(true);
        }
        else
        {
            inventorySystem.SetActive(false);
        }
    }
}
