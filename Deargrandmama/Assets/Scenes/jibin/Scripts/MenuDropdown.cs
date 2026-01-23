using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuDropdown : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject PauseOverlay;            // 전체 화면 일시정지 UI
    public TextMeshProUGUI PauseButtonText;    // 일시정지 버튼의 텍스트

    private bool IsMenuOpen = false;
    private bool IsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(false);
        PauseOverlay.SetActive(false);
        Time.timeScale = 1f;

        PauseButtonText.text = "Pause";//일시정지 -> 초기 텍스트
    }

      // ☰ 버튼(지금은 M버튼)
    public void ToggleMenu()
    {
        IsMenuOpen = !IsMenuOpen;
        MenuPanel.SetActive(IsMenuOpen);
    }

    //일시정지 버튼
    public void PauseGame()
    {
        if (IsPaused)
        {
             // 다시 시작
            Time.timeScale = 1f; //게임 재개
            IsPaused = false;

            PauseOverlay.SetActive(false); //오버레이 제거
            PauseButtonText.text = "Pause"; //텍스트 변경
        }
        else
        {
            //일시정지
            Time.timeScale = 0f; //게임 전체가 멈춤
            IsPaused = true;

            PauseOverlay.SetActive(true); //오버레이 표시
            PauseButtonText.text = "Restart"; //텍스트 변경
        }

        MenuPanel.SetActive(false); //메뉴 버튼 안의 패널 닫기
        IsMenuOpen = false;
    }

    // 그만두기 버튼
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("게임 종료");
    }
   
}
