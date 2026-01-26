using UnityEngine;
using UnityEngine.UI; // 이미지 제어를 위해 필수!

public class TimerController : MonoBehaviour
{
    public float totalTime = 20f; // 총 시간 (20초)
    private float currentTime;

    public Image timerFillImage; // 깎여나갈 시계 게이지 이미지

    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            currentTime = 0;
            if (timerFillImage != null) timerFillImage.fillAmount = 0;

            // 시간이 다 됐을 때 실행할 로직 (예: Debug.Log)
            Debug.Log("시간 종료!");
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
}