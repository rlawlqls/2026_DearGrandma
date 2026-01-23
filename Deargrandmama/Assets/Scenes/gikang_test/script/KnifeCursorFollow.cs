using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCursorFollow : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 offset;

    //  클릭 전엔 false, 클릭하면 true
    [SerializeField] private bool followOn = false;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    private void Update()
    {
        if (!followOn) return;   // ✅ 꺼져있으면 따라가지 않음

        Vector3 world = cam.ScreenToWorldPoint(Input.mousePosition);
        world.z = 0f;
        transform.position = world + offset;
    }

    // 다른 스크립트에서 켜고/끄기 위해 public API 제공
    public void SetFollow(bool on)
    {
        followOn = on;
        if (on) Cursor.visible = false;
        else Cursor.visible = true;
    }

    public bool IsFollowing => followOn;
}
