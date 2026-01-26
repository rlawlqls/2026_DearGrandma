using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StratMenu : MonoBehaviour
{
    // Play 버튼
    public void OnClickPlay()
    {
        SceneManager.LoadScene("Second");
    }

    // Quit 버튼
    public void OnClickQuit()
    {
        Application.Quit();
        Debug.Log("Game Quit"); // 에디터에서는 종료 안 되니까 로그로 확인
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
