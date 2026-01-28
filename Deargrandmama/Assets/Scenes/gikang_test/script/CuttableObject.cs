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

    [Header("Stage Settings")]
    [Tooltip("true면 마지막 스테이지를 강제로 지정합니다. (예: 7)")]
    [SerializeField] private bool useOverrideMaxStage = true;

    [Tooltip("마지막 스테이지 인덱스 (예: 7이면 0~7 총 8단계)")]
    [SerializeField] private int overrideMaxStage = 7;
    

    /* ==============================
     * 3. 이벤트
     * ============================== */

    [Header("Events")]
    public UnityEvent onStageChanged;
    public UnityEvent onFullyCut;

    /* ==============================
     * 4. 성공 연결 (선택)
     * ============================== */

    [Header("Optional - Timer Controller")]
    [Tooltip("완전 썰림 시 성공 패널을 띄우고 싶으면 연결하세요.")]
    [SerializeField] private CanOpenTimerController timerController;

    /* ==============================
     * 5. 내부 캐시
     * ============================== */

    private GameObject currentVisualInstance;
    private bool fullyCutInvoked = false; // 중복 호출 방지

    /* ==============================
     * 6. 초기화
     * ============================== */

    private void Awake()
    {
        if (visualRoot == null)
            visualRoot = transform;

        // 혹시 currentStage가 이상하게 저장돼 있어도 안전하게
        currentStage = Mathf.Clamp(currentStage, 0, GetMaxStage());

        ApplyStageVisual();

        // 시작부터 이미 완전 썰림 상태면(테스트용), 이벤트 처리
        if (IsFullyCut)
            InvokeFullyCutOnce();
    }

    /* ==============================
     * 7. 외부에서 쓰는 API
     * ============================== */

    /// <summary>현재 재료가 완전히 썰렸는지 여부</summary>
    public bool IsFullyCut => currentStage >= GetMaxStage();

    /// <summary>"썰기 1회 성공" 시 호출</summary>
    public void ApplyOneCut()
    {
        if (IsFullyCut) return;

        currentStage++;
        currentStage = Mathf.Clamp(currentStage, 0, GetMaxStage());

        ApplyStageVisual();
        onStageChanged?.Invoke();

        if (IsFullyCut)
            InvokeFullyCutOnce();

        Debug.Log($"[Cuttable] Stage = {currentStage}");


    }

    /// <summary>필요하면 외부에서 강제로 스테이지 세팅</summary>
    public void SetStage(int stage)
    {
        currentStage = Mathf.Clamp(stage, 0, GetMaxStage());
        ApplyStageVisual();
        onStageChanged?.Invoke();

        if (IsFullyCut)
            InvokeFullyCutOnce();
    }

    /* ==============================
     * 8. 내부 로직
     * ============================== */

    private int GetMaxStage()
    {
        int byPrefabs = (stagePrefabs != null && stagePrefabs.Length > 0) ? stagePrefabs.Length - 1 : 0;

        if (useOverrideMaxStage)
        {
            // 프리팹 길이가 override보다 짧으면 터질 수 있으니 안전하게 제한
            return Mathf.Min(overrideMaxStage, byPrefabs);
        }

        return byPrefabs;
    }

    private void InvokeFullyCutOnce()
    {
        if (fullyCutInvoked) return;
        fullyCutInvoked = true;

        onFullyCut?.Invoke();

        // 타이머 컨트롤러가 연결돼 있으면 성공 패널 띄우기

        Debug.Log("[Cuttable] Fully Cut!");
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

        if (resetLocalTransform)
        {
            currentVisualInstance.transform.localPosition = fixedLocalPosition;
            currentVisualInstance.transform.localRotation = Quaternion.Euler(fixedLocalRotationEuler);
            currentVisualInstance.transform.localScale = fixedLocalScale;
        }

        Debug.Log($"[Cuttable] Instantiated stage={idx} prefab={prefab.name} parent={visualRoot.name}");
    }
}
