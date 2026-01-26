using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class guideline : MonoBehaviour
{
    [Header("Points")]
    public Transform startPoint;
    public Transform endPoint;

    [Header("Tolerance")]
    public float tolerance = 0.20f;  // 처음엔 넉넉하게
    public int sampleCount = 12;

    [Header("Optional: Angle Check")]
    public bool checkAngle = true;
    public float maxAngleDiffDeg = 45f;

    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    private void LateUpdate()
    {
        if (startPoint == null || endPoint == null) return;

        lr.SetPosition(0, startPoint.position);
        lr.SetPosition(1, endPoint.position);
    }

    public bool IsSlashValid(Vector2 slashStart, Vector2 slashEnd)
    {
        if (startPoint == null || endPoint == null) return true;

        Vector2 a = startPoint.position;
        Vector2 b = endPoint.position;

        if (checkAngle)
        {
            Vector2 guideDir = (b - a).normalized;
            Vector2 slashDir = (slashEnd - slashStart).normalized;
            if (Vector2.Angle(guideDir, slashDir) > maxAngleDiffDeg) return false;
        }

        int n = Mathf.Max(2, sampleCount);
        for (int i = 0; i < n; i++)
        {
            float t = i / (float)(n - 1);
            Vector2 p = Vector2.Lerp(slashStart, slashEnd, t);

            if (DistancePointToSegment(p, a, b) > tolerance)
                return false;
        }

        return true;
    }

    private float DistancePointToSegment(Vector2 p, Vector2 a, Vector2 b)
    {
        Vector2 ab = b - a;
        float ab2 = ab.sqrMagnitude;
        if (ab2 < 1e-6f) return Vector2.Distance(p, a);

        float t = Vector2.Dot(p - a, ab) / ab2;
        t = Mathf.Clamp01(t);
        Vector2 proj = a + t * ab;
        return Vector2.Distance(p, proj);
    }
}
