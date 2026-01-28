using System.Collections.Generic;
using UnityEngine;

public class PenchCutter : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PenchCursorFollow follow;
    [SerializeField] private Camera cam;

    [Header("Layers")]
    [SerializeField] private LayerMask cuttableLayer;

    [Header("Rules")]
    [SerializeField] private float minDragDistance = 0.6f;
    [SerializeField] private float startCheckRadius = 0.05f;
    [SerializeField] private float cutCooldown = 0.10f;

    [Header("Visual")]
    [SerializeField] private LineRenderer dragLine;
    [SerializeField] private float pointMinDistance = 0.05f;

    // ✅ 불필요한 List 삭제하고 하나의 매니저만 사용하거나, 
    // 필요하다면 매니저에서 현재 라인을 가져오도록 단순화함
    [Header("Manager")]
    [SerializeField] private GuideLineManager lineManager;

    private List<Vector3> dragPoints = new List<Vector3>();
    private Vector2 dragStartWorld;
    private bool isDragging;
    private float lastCutTime = -999f;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
        if (follow == null) follow = GetComponent<PenchCursorFollow>();
        if (dragLine != null) dragLine.enabled = false;
    }

    private void Update()
    {
        // 펜치 기능이 활성화(followOn) 되었을 때만 작동
        if (follow != null && !follow.followOn) return;

        if (Input.GetMouseButtonDown(0)) StartDrag();
        if (isDragging) UpdateDragLine();
        if (Input.GetMouseButtonUp(0) && isDragging) EndDragAndCut();
    }

    private void StartDrag()
    {
        isDragging = true;
        Vector3 start = cam.ScreenToWorldPoint(Input.mousePosition);
        start.z = 0f;
        dragStartWorld = start;

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
        if (Vector2.Distance(dragStartWorld, dragEndWorld) < minDragDistance) return;
        if (Time.time - lastCutTime < cutCooldown) return;

        // ✅ 에러 방지: lineManager가 있는지, 그 안에 CurrentLine이 있는지 확인
        if (lineManager == null || lineManager.CurrentLine == null)
        {
            Debug.LogWarning("LineManager 또는 CurrentLine이 설정되지 않았습니다.");
            return;
        }

        guideline currentLine = lineManager.CurrentLine;

        // 가이드라인 판정 (IsSlashValid 호출)
        if (currentLine.IsSlashValid(dragStartWorld, dragEndWorld))
        {
            // 가이드라인 중심부에서 재료(CuttableObject) 감지
            Vector2 mid = (currentLine.startPoint.position + currentLine.endPoint.position) * 0.5f;
            float r = currentLine.tolerance + 0.1f;

            Collider2D col = Physics2D.OverlapCircle(mid, r, cuttableLayer);
            if (col != null)
            {
                CuttableObject cuttable = col.GetComponent<CuttableObject>();
                if (cuttable != null)
                {
                    cuttable.ApplyOneCut();
                    lastCutTime = Time.time;
                    lineManager.Advance(); // 성공 시 다음 가이드라인으로
                }
            }
        }
    }
}