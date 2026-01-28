using System.Collections.Generic;
using UnityEngine;

public class PenchCutter : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PenchCursorFollow follow;
    [SerializeField] private Camera cam;

    [Header("Visual")]
    [SerializeField] private LineRenderer dragLine; // 선을 그릴 라인 렌더러
    [SerializeField] private float pointMinDistance = 0.05f; // 점 사이의 최소 간격

    private List<Vector3> dragPoints = new List<Vector3>();
    private bool isDragging;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
        if (follow == null) follow = GetComponent<PenchCursorFollow>();

        // 시작할 때 라인 초기화
        if (dragLine != null)
        {
            dragLine.enabled = false;
            dragLine.positionCount = 0;
        }
    }

    private void Update()
    {
        // 펜치를 들고 있을 때만 작동
        if (follow != null && !follow.followOn) return;

        // 마우스 클릭 시 그리기 시작
        if (Input.GetMouseButtonDown(0)) StartDrag();

        // 드래그 중 선 업데이트
        if (isDragging) UpdateDragLine();

        // 마우스 떼면 그리기 종료
        if (Input.GetMouseButtonUp(0)) EndDrag();
    }

    private void StartDrag()
    {
        isDragging = true;
        dragPoints.Clear();

        Vector3 startPos = GetMouseWorldPosition();
        dragPoints.Add(startPos);

        if (dragLine != null)
        {
            dragLine.enabled = true;
            dragLine.positionCount = 1;
            dragLine.SetPosition(0, startPos);
        }
    }

    private void UpdateDragLine()
    {
        if (dragLine == null) return;

        Vector3 currentPos = GetMouseWorldPosition();

        // 마지막 점과 현재 마우스 위치가 일정 거리 이상일 때만 점 추가 (최적화)
        if (dragPoints.Count > 0 &&
            Vector3.Distance(dragPoints[dragPoints.Count - 1], currentPos) < pointMinDistance)
            return;

        dragPoints.Add(currentPos);
        dragLine.positionCount = dragPoints.Count;
        dragLine.SetPosition(dragPoints.Count - 1, currentPos);
    }

    private void EndDrag()
    {
        isDragging = false;
        // 필요하다면 여기서 선을 지우지 않고 유지할 수도 있어. 
        // 만약 마우스를 뗄 때 선을 바로 지우고 싶다면 아래 주석을 해제해.
        // if (dragLine != null) dragLine.enabled = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        // 카메라와의 거리 (2D 씬 환경에 맞춰 조정)
        mousePos.z = Mathf.Abs(cam.transform.position.z);
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        worldPos.z = -1f; // 2D 평면 고정
        return worldPos;
    }
}