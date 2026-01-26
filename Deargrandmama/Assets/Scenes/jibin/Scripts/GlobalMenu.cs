using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalMenu : MonoBehaviour
{
    public GameObject menuPanel;
    private bool isPaused = false;
    private static GlobalMenu instance;

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
    public void ToggleMenu()
    {
        isPaused = !isPaused;
        menuPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void Resume()
    {
        isPaused = false;
        menuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}