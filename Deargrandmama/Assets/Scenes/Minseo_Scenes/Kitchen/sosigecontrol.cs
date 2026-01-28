using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 이미지 제어를 위해 필수!
using UnityEngine.SceneManagement;

public class sosigecontrol : MonoBehaviour
{
 
    public float totalTime = 20f; // 총 시간 (20초)
    private float currentTime;

    public Image timerFillImage; // 깎여나갈 시계 게이지 이미지

    public GameObject timeoutPanel;
    public GameObject successPanel;   // ✅ 성공 패널
    private bool isFinished = false;  // ✅ 게임 종료 여부

    void Start()
    {
        currentTime = totalTime;
        if (timeoutPanel != null) timeoutPanel.SetActive(false);
        if (successPanel != null) successPanel.SetActive(false); 
    }

    void Update()
    {
        if (isFinished) return;
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            currentTime = 0;
            if (timerFillImage != null) timerFillImage.fillAmount = 0;

            ShowTimeout();
        }
    }
    /// <summary>
/// 성공했을 때 외부 스크립트에서 호출
/// </summary>
public void OnCutSuccess()
{
    if (isFinished) return;
    isFinished = true;

    // 혹시 떠있을 타임아웃 패널 끄기
    if (timeoutPanel != null)
        timeoutPanel.SetActive(false);

    // 성공 패널 켜기
    if (successPanel != null)
        successPanel.SetActive(true);

    // 필요하면 게임 멈춤
    // Time.timeScale = 0;
}
    void ShowTimeout()
{
    if (isFinished) return;
    isFinished = true;

    if (timeoutPanel != null && !timeoutPanel.activeSelf)
    {
        timeoutPanel.SetActive(true);
        // Time.timeScale = 0;
    }
}


    void UpdateTimerUI()
    {
        if (timerFillImage != null)
        {
            float maxFill = 0.33f;
            // 현재 남은 시간을 총 시간으로 나눠서 0~1 사이 값으로 변환
            timerFillImage.fillAmount = (currentTime / totalTime) * maxFill;
        }
    }

 public void OnNextGameButtonClick()
    {
        // 1. 다음 단계(1번: 양파 썰기)로 진행도 저장
        PlayerPrefs.SetInt("RecipeStep", 1);
        PlayerPrefs.Save(); // 데이터 안전하게 저장

        // 2. 다시 요리책 씬으로 이동 (씬 이름 대소문자 주의!)
        SceneManager.LoadScene("RecipeBook");
    }
    
}
