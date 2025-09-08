using UnityEngine;

public class bgm : MonoBehaviour
{
    static bgm instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;        // if it doesnt reference anything make this = the bgm
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);        // if there is already smth the pointer is pointing at then destroy it (to prevent duplicates)
        }
    }
}
