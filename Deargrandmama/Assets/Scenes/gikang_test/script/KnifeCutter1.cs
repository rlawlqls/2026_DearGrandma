using System.Collections.Generic;
using UnityEngine;

public class KnifeCutter1 : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private KnifeCursorFollow follow;
    [SerializeField] private Camera cam;
    [SerializeField] private guideline guideLine; // ✅ GuideLine로 수정

    [Header("Layers")]
    [SerializeField] private LayerMask cuttableLayer;

    [Header("Rules")]
    [SerializeField] private float minDragDistance = 0.6f;
    [SerializeField] private float slashRadius = 0.12f;
    [SerializeField] private float startCheckRadius = 0.05f;
    [SerializeField] private float cutCooldown = 0.10f;

    [Header("Visual")]
    [SerializeField] private LineRenderer dragLine; // 드래그 선 시각화
    [SerializeField] private float pointMinDistance = 0.05f; // 점 사이 최소 거리

    [SerializeField] private List<GuideLineManager> lineManagers = new List<GuideLineManager>();

    
    private GuideLineManager lineManager;
    private List<Vector3> dragPoints = new List<Vector3>();
    private Vector2 dragStartWorld;
    private bool isDragging;
    private float lastCutTime = -999f;

    private CuttableObject startCuttable;
    private int currentLineIndex = 0;
    private void Awake()
    {
        if (cam == null) cam = Camera.main;
        if (follow == null) follow = GetComponent<KnifeCursorFollow>();

        // 시작 시 드래그 라인 숨김
        if (dragLine != null) dragLine.enabled = false;
    }
    private void Start()
    {
        lineManager = lineManagers[currentLineIndex];
        
    }

    private void Update()
    {
        // ✅ 칼 장착 상태일 때만 작동
        if (follow != null && !follow.followOn) return;

        // 1) 드래그 시작
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }

        // 2) 드래그 중(선 업데이트)
        if (isDragging)
        {
            UpdateDragLine();
        }

        // 3) 드래그 종료 + 컷 판정
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            EndDragAndCut();
        }
    }

    private void StartDrag()
{
    isDragging = true;

    Vector3 start = cam.ScreenToWorldPoint(Input.mousePosition);
    start.z = 0f;
    dragStartWorld = start;

    Collider2D hit = Physics2D.OverlapCircle(dragStartWorld, startCheckRadius, cuttableLayer);
    startCuttable = hit ? hit.GetComponent<CuttableObject>() : null;

    // 🔥 점 리스트 초기화
    dragPoints.Clear();
    dragPoints.Add(start);

    if (dragLine != null)
    {
        dragLine.enabled = true;
        dragLine.positionCount = 1;
        dragLine.SetPosition(0, start);
    }
}

   private void UpdateDragLine()
{
    if (dragLine == null) return;

    Vector3 current = cam.ScreenToWorldPoint(Input.mousePosition);
    current.z = 0f;

    // 마지막 점과 너무 가까우면 추가 안 함
    if (dragPoints.Count > 0 &&
        Vector3.Distance(dragPoints[dragPoints.Count - 1], current) < pointMinDistance)
        return;

    dragPoints.Add(current);

    dragLine.positionCount = dragPoints.Count;
    dragLine.SetPosition(dragPoints.Count - 1, current);
}

private void EndDragAndCut()
{
    isDragging = false;
    if (dragLine != null) dragLine.enabled = false;

    if (dragPoints.Count < 2) return;

    Vector2 dragEndWorld = (Vector2)dragPoints[dragPoints.Count - 1];
    float dist = Vector2.Distance(dragStartWorld, dragEndWorld);
    if (dist < minDragDistance) return;

    if (Time.time - lastCutTime < cutCooldown) return;

    // 1) 현재 가이드라인
    guideline guideLine = lineManager != null ? lineManager.CurrentLine : null;
    if (guideLine == null) return;

    // 2) 가이드라인 판정
    if (!guideLine.IsSlashValid(dragStartWorld, dragEndWorld))
        return;

    // 3) ✅ "가이드라인 근처"의 재료를 찾아서 자르기
    Vector2 a = guideLine.startPoint.position;
    Vector2 b = guideLine.endPoint.position;
    Vector2 mid = (a + b) * 0.5f;

    // 검색 반경: tolerance + 약간
    float r = Mathf.Max(guideLine.tolerance, 0.05f) + 0.05f;

    Collider2D col = Physics2D.OverlapCircle(mid, r, cuttableLayer);
    if (col == null) return;

    CuttableObject cuttable = col.GetComponent<CuttableObject>();
    if (cuttable == null) return;

    cuttable.ApplyOneCut();
    lastCutTime = Time.time;

    // 4) 다음 가이드라인으로 이동
    lineManager.Advance();
    currentLineIndex++;
    if (currentLineIndex < lineManagers.Count)
    {
        lineManager = lineManagers[currentLineIndex];
    }
}


}
