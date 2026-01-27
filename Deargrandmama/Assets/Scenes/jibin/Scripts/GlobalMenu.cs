using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalMenu : MonoBehaviour
{
    private static GlobalMenu instance;

    void Start()
    {
        Debug.Log("MenuCanvas 살아있음: " + gameObject.name);
    }
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}