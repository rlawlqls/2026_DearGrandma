using UnityEngine;

public class PenchCursorFollow : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public bool followOn = false;

    [Header("Visual Settings")]
    [SerializeField] private Transform penchVisual; // ❗여기에 자식인 pench를 꼭 넣어줘
    [SerializeField] private Vector3 visualOffset;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
        UpdateVisualOffset();
    }

    private void Update()
    {
        if (!followOn) return;

        // 마우스의 스크린 좌표
        Vector3 mousePos = Input.mousePosition;

        // 2D 씬이므로 카메라와의 거리를 기반으로 월드 좌표 변환
        // 카메라 Z가 보통 -10이므로 10만큼의 깊이를 줌
        mousePos.z = Mathf.Abs(cam.transform.position.z);

        Vector3 targetPos = cam.ScreenToWorldPoint(mousePos);
        targetPos.z = 0f; // 2D 평면 고정

        // 부모 위치를 마우스 끝으로 이동
        transform.position = targetPos;
    }

    private void UpdateVisualOffset()
    {
        if (penchVisual != null)
        {
            penchVisual.localPosition = visualOffset;
        }
    }

    private void OnValidate()
    {
        UpdateVisualOffset();
    }

    public void SetFollow(bool on)
    {
        followOn = on;
        Cursor.visible = !on;
    }
}