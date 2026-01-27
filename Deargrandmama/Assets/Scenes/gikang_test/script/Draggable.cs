using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
     [Header("Refs")]
    [SerializeField] private Camera cam;
    [SerializeField] private toolmanager toolManager; // 칼 들고 있으면 드래그 막고 싶을 때

    [Header("Drag Feel")]
    [SerializeField] private float followLerp = 25f; // 클수록 손에 붙는 느낌
    [SerializeField] private bool bringToFrontWhileDragging = true;

    private bool dragging = false;
    private Vector3 grabOffset;
    private Vector3 targetPos;

    private int originalSortingOrder;
    private SpriteRenderer sr;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        if (sr != null) originalSortingOrder = sr.sortingOrder;
    }

    private void OnMouseDown()
    {
        // 칼 들고 있으면 재료 드래그 못 하게
        if (toolManager != null && toolManager.IsKnifeEquipped())
            return;

        dragging = true;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;

        grabOffset = transform.position - mouseWorld;
        targetPos = transform.position;

        if (bringToFrontWhileDragging && sr != null)
            sr.sortingOrder = 200; // 드래그 중 위로
    }

    private void OnMouseDrag()
    {
        if (!dragging) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;

        targetPos = mouseWorld + grabOffset;
    }

    private void OnMouseUp()
    {
        if (!dragging) return;
        dragging = false;

        if (sr != null)
            sr.sortingOrder = originalSortingOrder;

        // 여기서 “드랍존 스냅” 같은 후처리도 가능
    }

    private void Update()
    {
        if (!dragging) return;

        // 부드럽게 따라오게
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followLerp);
    }
}
