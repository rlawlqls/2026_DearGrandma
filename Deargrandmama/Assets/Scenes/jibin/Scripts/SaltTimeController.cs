using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaltTimeController : MonoBehaviour
{
    public float totalTime = 20f; // 
    private float currentTime;

    public Image timerFillImage;
    public GameObject timeoutPanel;
    public GameObject SaltSuccessPanel;

    private bool isSuccess = false;
    void Start()
    {
        currentTime = totalTime;

        if (timeoutPanel != null)
            timeoutPanel.SetActive(false);

        if (SaltSuccessPanel != null)
            SaltSuccessPanel.SetActive(false); // 🔥 시작 시 숨김currentTime = totalTime;

    }

    void Update()
    {
        if (isSuccess) return; // 🔥 성공했으면 아무것도 안 함

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            currentTime = 0;
            if (timerFillImage != null)
                timerFillImage.fillAmount = 0;

            ShowTimeout();
        }
    }

    void ShowTimeout()
    {
        if (timeoutPanel != null && !timeoutPanel.activeSelf)
        {
            timeoutPanel.SetActive(true);
            Debug.Log("⏰ 타임 아웃!");
        }
    }

    void UpdateTimerUI()
    {
        if (timerFillImage != null)
        {
            float maxFill = 0.33f;
            timerFillImage.fillAmount = (currentTime / totalTime) * maxFill;
        }
    }

    // ✅ 흔들기 성공 시 호출
    public void StopTimer()
    {
        isSuccess = true; // ✅ 성공 표시
        currentTime = 0;

        if (timerFillImage != null)
            timerFillImage.fillAmount = 0;

        // 🔥 성공 패널 표시
        if (SaltSuccessPanel != null)
            SaltSuccessPanel.SetActive(true);

        Debug.Log("⏱ 타이머 멈춤 (성공)");
    }

    }
