using UnityEngine;

public class Paused : MonoBehaviour
{
    [SerializeField] GameObject pausetextobject;
    private bool isPaused = false;
    

    private void Update()
    {
        checkforinput();
    }

    void checkforinput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
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
    }
}
