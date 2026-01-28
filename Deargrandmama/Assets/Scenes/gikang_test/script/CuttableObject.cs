using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CuttableObject : MonoBehaviour
{
    /* ==============================
     * 1. 비주얼 데이터 (Prefab Stage)
     * ============================== */

    [Header("Visual Root (Optional)")]
    [Tooltip("단계 프리팹이 생성될 부모 Transform. 비워두면 이 오브젝트 자신이 부모가 됩니다.")]
    [SerializeField] private Transform visualRoot;

    [Header("Stage Prefabs")]
    [Tooltip("0단계(원본) ~ 마지막 단계(완전 썰림) 까지 순서대로 프리팹을 넣으세요.")]
    [SerializeField] private GameObject[] stagePrefabs;

    [Header("Keep Local Transform")]
    [Tooltip("각 단계 프리팹 생성 시 localPosition/localRotation/localScale 을 고정할지 여부")]
    [SerializeField] private bool resetLocalTransform = true;

    [SerializeField] private Vector3 fixedLocalPosition = Vector3.zero;
    [SerializeField] private Vector3 fixedLocalRotationEuler = Vector3.zero;
    [SerializeField] private Vector3 fixedLocalScale = Vector3.one;

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
     * 4. 내부 캐시
     * ============================== */

    private GameObject currentVisualInstance;

    /* ==============================
     * 5. 초기화
     * ============================== */

    private void Awake()
    {
        if (visualRoot == null)
            visualRoot = transform;

        ApplyStageVisual();
    }

    /* ==============================
     * 6. 외부에서 쓰는 API
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
     * 7. 내부 로직
     * ============================== */

    private int GetMaxStage()
    {
        if (stagePrefabs != null && stagePrefabs.Length > 0)
            return stagePrefabs.Length - 1;

        return 0;
    }

private void ApplyStageVisual()
{
    if (currentVisualInstance != null)
    {
        Destroy(currentVisualInstance);
        currentVisualInstance = null;
    }

    if (stagePrefabs == null || stagePrefabs.Length == 0)
    {
        Debug.LogWarning("[Cuttable] stagePrefabs is empty!");
        return;
    }

    int idx = Mathf.Clamp(currentStage, 0, stagePrefabs.Length - 1);
    GameObject prefab = stagePrefabs[idx];

    if (prefab == null)
    {
        Debug.LogWarning($"[Cuttable] stagePrefabs[{idx}] is null!");
        return;
    }

    currentVisualInstance = Instantiate(prefab, visualRoot);
    Debug.Log($"[Cuttable] Instantiated stage={idx} prefab={prefab.name} parent={visualRoot.name}");

    if (resetLocalTransform)
    {
        currentVisualInstance.transform.localPosition = fixedLocalPosition;
        currentVisualInstance.transform.localRotation = Quaternion.Euler(fixedLocalRotationEuler);
        currentVisualInstance.transform.localScale = fixedLocalScale;
    }
}
}
