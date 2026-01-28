using UnityEngine;
using System.Collections;

public class CanOpener : MonoBehaviour
{
    [Header("Scripts Refs")]
    [SerializeField] private CircleGuideline circleGuide;
    [SerializeField] private PenchCursorFollow penchFollow;
    [SerializeField] private CanOpenTimerController timerController;

    [Header("Can Objects")]
    [SerializeField] private GameObject closedCan;
    [SerializeField] private GameObject openedCan;

    [Header("UI Settings")]
    [SerializeField] private GameObject successPanel;
    [SerializeField] private GameObject timeoutPanel; // ✅ 여기에 타임아웃 패널도 연결해줘!
    [SerializeField] private float openThreshold = 0.95f;

    private bool isDone = false;

    void Update()
    {
        if (isDone || penchFollow == null || circleGuide == null) return;

        if (penchFollow.followOn && Input.GetMouseButton(0))
        {
            float progress = circleGuide.GetRotationProgress(Input.mousePosition);

            if (progress >= openThreshold)
            {
                StartSuccessProcess();
            }
        }
    }

    void StartSuccessProcess()
    {
        isDone = true;

        // ✅ 1. 타이머 컨트롤러 스크립트를 즉시 비활성화 (업데이트 중지)
        if (timerController != null)
        {
            timerController.enabled = false;
            Debug.Log("타이머 스크립트 정지");
        }

        // ✅ 2. 혹시라도 떠 있을지 모르는 타임아웃 패널을 강제로 끔
        if (timeoutPanel != null)
        {
            timeoutPanel.SetActive(false);
            Debug.Log("타임아웃 패널 강제 비활성화");
        }

        StartCoroutine(OpenSequence());
    }

    IEnumerator OpenSequence()
    {
        if (closedCan != null) closedCan.SetActive(false);
        if (openedCan != null) openedCan.SetActive(true);

        // 2초 기다리는 동안 절대 타임아웃이 뜨지 않음
        yield return new WaitForSeconds(1f);

        if (successPanel != null)
        {
            successPanel.SetActive(true);
            Debug.Log("성공 패널 표시!");
        }
    }
}