using UnityEngine;

public class PenchCursorFollow : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public bool followOn = false;

    [Header("Visual Offset Settings")]
    [SerializeField] private Transform penchVisual; // 펜치 이미지가 들어있는 자식 오브젝트
    [SerializeField] private Vector3 visualOffset; // 마우스 커서와 펜치 날 사이의 간격 조정

    private void Awake()
    {
        if (cam == null) cam = Camera.main;

        // 시작할 때 오프셋 적용
        UpdateVisualOffset();
    }

    private void Update()
    {
        if (!followOn) return;

        // 1. 마우스의 스크린 좌표를 가져옴
        Vector3 mousePos = Input.mousePosition;

        // 2. 카메라와의 거리를 고려하여 월드 좌표로 변환 (보통 10f가 적당함)
        mousePos.z = 10f;
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);

        // 3. 2D 게임이므로 Z축은 0으로 고정
        worldPos.z = 0f;

        // 4. 부모(이 오브젝트)를 마우스 위치로 이동
        transform.position = worldPos;
    }

    private void UpdateVisualOffset()
    {
        if (penchVisual != null)
        {
            penchVisual.localPosition = visualOffset;
        }
    }

    // 인스펙터에서 값을 수정하면 실시간으로 반영되게 함
    private void OnValidate()
    {
        UpdateVisualOffset();
    }

    // 다른 스크립트(ToolManager 등)에서 펜치를 켜고 끌 때 사용
    public void SetFollow(bool on)
    {
        followOn = on;
        Cursor.visible = !on; // 펜치를 쓰면 기본 마우스 커서는 숨김
    }
}