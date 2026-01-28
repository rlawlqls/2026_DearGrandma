using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SaltController : MonoBehaviour
{
    [SerializeField] private SaltTimeController timeController;
    [SerializeField] private GameObject SaltSuccessPanel; // ✅ 성공 패널 추가

    [Header("Sprites")]
    public Sprite normalSalt;
    public Sprite leanSalt;
    public Sprite flowSalt;

    [Header("Shake Settings")]
    public float flowTime = 1f;
    public float successTime = 3f;
    public float moveThreshold = 0.01f;
    public float stopTolerance = 0.15f;

    private SpriteRenderer sr;
    private Vector3 lastPos;

    private bool isNearPot = false;
    private bool isCompleted = false;

    private float shakeTimer = 0f;
    private float stopTimer = 0f;

    private bool flowLogged = false;
    private bool successLogged = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = normalSalt;
        lastPos = transform.position;

        if (SaltSuccessPanel != null)
            SaltSuccessPanel.SetActive(false); // 시작 시 숨김
    }

    void Update()
    {
        if (!isNearPot || isCompleted) return;

        float moveDist = Vector3.Distance(transform.position, lastPos);

        if (moveDist >= moveThreshold)
        {
            shakeTimer += Time.deltaTime;
            stopTimer = 0f;

            if (shakeTimer >= flowTime && !flowLogged)
            {
                flowLogged = true;
                Debug.Log("🧂 소금 흔들기 1초 도달");
                sr.sprite = flowSalt;
            }

            if (shakeTimer >= successTime && !successLogged)
            {
                successLogged = true;
                Debug.Log("✅ 소금 흔들기 3초 성공!");
                CompleteSalt();
            }
        }
        else
        {
            stopTimer += Time.deltaTime;

            if (stopTimer >= stopTolerance)
            {
                if (shakeTimer > 0f) // 🔥 추가
                {
                    Debug.Log("❌ 흔들기 중단 (타이머 리셋)");
                }

                shakeTimer = 0f;
                stopTimer = 0f;
                flowLogged = false;

                if (!isCompleted)
                    sr.sprite = leanSalt;
            }
        }
        lastPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pot"))
        {
            isNearPot = true;
            shakeTimer = 0f;
            stopTimer = 0f;
            flowLogged = false;
            successLogged = false;

            sr.sprite = leanSalt;
            Debug.Log("🍲 냄비 영역 진입");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pot"))
        {
            isNearPot = false;
            shakeTimer = 0f;
            stopTimer = 0f;
            flowLogged = false;
            successLogged = false;

            if (!isCompleted)
                sr.sprite = normalSalt;

            Debug.Log("⬅️ 냄비 영역 이탈");
        }
    }

    void CompleteSalt()
    {
        isCompleted = true;
        sr.sprite = flowSalt;

        Debug.Log("🎉 Salt Added Successfully!");

        // 1️⃣ 타이머 멈추기
        if (timeController != null)
            timeController.StopTimer();

        // 2️⃣ 드래그 비활성화
        Draggable drag = GetComponent<Draggable>();
        if (drag != null)
            drag.enabled = false;

        // 3️⃣ 성공 패널 표시
        if (SaltSuccessPanel != null)
            SaltSuccessPanel.SetActive(true);

      
    }

    
}
