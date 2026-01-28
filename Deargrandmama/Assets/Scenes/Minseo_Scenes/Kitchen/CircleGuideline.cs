using UnityEngine;

public class CircleGuideline : MonoBehaviour
{
    [Header("Guideline Settings")]
    public RectTransform guideRect; // 인스펙터에서 'Image' 오브젝트 연결
    public float radius = 100f;
    public float tolerance = 50f;

    private float currentAngle = 0f;
    private float totalRotation = 0f;
    private Vector2 lastVector = Vector2.up;

    public float GetRotationProgress(Vector2 mousePos)
    {
        Vector2 localPos;
        // 마우스 좌표를 가이드라인 기준 로컬 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(guideRect, mousePos, null, out localPos);

        float dist = localPos.magnitude;

        // 1. 가이드라인(원) 위에 있는지 확인
        if (Mathf.Abs(dist - radius) < tolerance)
        {
            Vector2 currentVector = localPos.normalized;
            float angleStep = Vector2.SignedAngle(lastVector, currentVector);

            // 2. 한 방향으로만 회전하도록 누적 (급격한 튀기 방지)
            if (Mathf.Abs(angleStep) < 45f)
            {
                totalRotation += Mathf.Abs(angleStep);
            }
            lastVector = currentVector;
        }

        // 360도 기준으로 0~1 사이 값 반환
        return Mathf.Clamp01(totalRotation / 360f);
    }

    public void ResetProgress()
    {
        totalRotation = 0f;
    }
}