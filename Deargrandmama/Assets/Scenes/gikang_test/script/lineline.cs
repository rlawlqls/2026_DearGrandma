using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lineline : MonoBehaviour
{
    [Header("Points")]
    public Transform startPoint;
    public Transform endPoint;

    [Header("Visual")]
    [Tooltip("선 두께")]
    public float lineWidth = 3.0f;

    [Tooltip("점선 반복 횟수 (세로 기준)")]
    public float repeatCount = 3.0f;

    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();

        // 기본 설정
        lr.useWorldSpace = true;
        lr.positionCount = 2;
        lr.textureMode = LineTextureMode.Tile;


        // 두께
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        // 머티리얼 인스턴스화 (다른 라인 영향 방지)
        lr.material = Instantiate(lr.material);

        ApplyTiling();
    }

    void Update()
    {
        if (startPoint == null || endPoint == null) return;

        // 위치 갱신
        lr.SetPosition(0, startPoint.position);
        lr.SetPosition(1, endPoint.position);
    }

    void ApplyTiling()
    {
        // 세로 점선 기준 → Y축 반복
        lr.material.mainTextureScale = new Vector2(1f, repeatCount);
        lr.material.SetTextureScale("_BaseMap", new Vector2(1f, repeatCount));
    }

#if UNITY_EDITOR
    // Inspector에서 값 바꿀 때 즉시 반영
    void OnValidate()
    {
        if (lr == null) lr = GetComponent<LineRenderer>();

        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        if (lr.material != null)
        {
            ApplyTiling();
        }
    }
#endif
}
