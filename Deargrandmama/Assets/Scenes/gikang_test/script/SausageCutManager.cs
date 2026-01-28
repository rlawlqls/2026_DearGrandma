using UnityEngine;

public class SausageCutManager : MonoBehaviour
{
    [Header("Cut Settings")]
    [Tooltip("전체(소시지 2개 합산) 몇 번 썰면 성공인지")]
    public int requiredCuts = 8;

    private int totalCutCount = 0;
    private bool successSent = false;

    [Header("Timer (Success UI Controller)")]
    [Tooltip("성공 패널을 띄우는 타이머/컨트롤러 스크립트")]
    [SerializeField] private sosigecontrol timerController;

    public void RegisterCut()
    {
        if (successSent) return;

        totalCutCount++;
        Debug.Log($"[SausageManager] Total Cut = {totalCutCount} / {requiredCuts}");

        if (totalCutCount >= requiredCuts)
        {
            successSent = true;
            Debug.Log("[SausageManager] ✅ SUCCESS!");

            if (timerController != null)
            {
                //timerController.StartSuccessProcess(); 
                // ⚠️ 만약 너 타이머 스크립트에 StartSuccessProcess가 없고 OnCutSuccess만 있으면
                // 아래 줄로 바꿔:
                timerController.OnCutSuccess();
            }
            else
            {
                Debug.LogWarning("[SausageManager] ❌ timerController 연결 안 됨 (Inspector에서 넣어야 함)");
            }
        }
    }

    // (옵션) 재시작 시 호출하고 싶으면
    public void ResetCount()
    {
        totalCutCount = 0;
        successSent = false;
        Debug.Log("[SausageManager] ResetCount()");
    }
}
