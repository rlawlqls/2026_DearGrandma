using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CuttableObject : MonoBehaviour
{
    
    /* ==============================
     * 1. 비주얼 데이터
     * ============================== */

    [Header("Renderer")]
    [SerializeField] private SpriteRenderer targetRenderer;

    [Header("Stage Sprites (Optional)")]
    [SerializeField] private Sprite[] stageSprites;

    [Header("Stage Colors (Debug / Placeholder)")]
    [SerializeField] private Color[] stageColors;

    /* ==============================
     * 2. 진행 상태
     * ============================== */

    [Header("Progress")]
    [SerializeField] private int currentStage = 0;

    /* ==============================
     * 3. 이벤트
     * ============================== */

    [Header("Events")]
    public UnityEvent onStageChanged;
    public UnityEvent onFullyCut;

    /* ==============================
     * 4. 초기화
     * ============================== */

    private void Awake()
    {
        // SpriteRenderer 자동 할당
        if (targetRenderer == null)
            targetRenderer = GetComponent<SpriteRenderer>();

        ApplyStageVisual();
    }

    /* ==============================
     * 5. 외부에서 쓰는 API
     * ============================== */

    /// <summary>
    /// 현재 재료가 완전히 썰렸는지 여부
    /// </summary>
    public bool IsFullyCut
    {
        get
        {
            int maxStage = GetMaxStage();
            return currentStage >= maxStage;
        }
    }

    /// <summary>
    /// "썰기 1회 성공" 시 호출
    /// </summary>
    public void ApplyOneCut()
    {
        if (IsFullyCut) return;

        currentStage++;
        currentStage = Mathf.Clamp(currentStage, 0, GetMaxStage());

        ApplyStageVisual();
        onStageChanged?.Invoke();

        if (IsFullyCut)
            onFullyCut?.Invoke();
        Debug.Log($"Stage = {currentStage}");
    }

    /* ==============================
     * 6. 내부 로직
     * ============================== */

    private int GetMaxStage()
    {
        if (stageSprites != null && stageSprites.Length > 0)
            return stageSprites.Length - 1;

        if (stageColors != null && stageColors.Length > 0)
            return stageColors.Length - 1;

        return 0;
    }

   private void ApplyStageVisual()
{
    if (targetRenderer == null) return;

    // Sprite 적용(있으면)
    if (stageSprites != null && stageSprites.Length > 0)
    {
        int idx = Mathf.Clamp(currentStage, 0, stageSprites.Length - 1);
        targetRenderer.sprite = stageSprites[idx];
    }

    // Color 적용(있으면)
    if (stageColors != null && stageColors.Length > 0)
    {
        int idx = Mathf.Clamp(currentStage, 0, stageColors.Length - 1);
        targetRenderer.color = stageColors[idx];
        Debug.Log($"Applied Color idx={idx} color={stageColors[idx]}");
    }
}
    
}
