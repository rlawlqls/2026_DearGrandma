using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RhythmBoilController : MonoBehaviour
{
    [Header("Rhythm UI")]
    public RectTransform barArea;
    public RectTransform successZone;
    public RectTransform movingBar;

    [Header("Pot")]
    public Image potImage;
    public Sprite potInNu1;
    public Sprite nuddleLeft;
    public Sprite potInNu2;

    [Header("Progress UI")]
    public Image[] progressSlots; // 5개
    public Color successColor = Color.green;
    public Color defaultColor = Color.gray;

    [Header("Panels")]
    public GameObject rhythmGamePanel;
    public GameObject BoilSuccessPanel;
    public GameObject BoilFailPanel;

    [Header("Settings")]
    public float barSpeed = 400f;
    public int maxRounds = 5;

    private int successCount = 0;
    private bool isPlaying = false;
    private bool movingRight = true;
    private float minX, maxX;

    private bool inputLocked = false;
    private int attemptCount = 0;

    void Start()
    {
        rhythmGamePanel.SetActive(false);
        BoilSuccessPanel.SetActive(false);
        BoilFailPanel.SetActive(false);

        successZone.gameObject.SetActive(true); // ⭐ 노란 영역 항상 켜기

        foreach (var slot in progressSlots)
        {
            slot.gameObject.SetActive(false);
            slot.color = defaultColor;
        }
    }

    void Update()
    {
        if (!isPlaying || inputLocked) return;

        MoveBar();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSuccess();
        }
    }

    // 🍝 면을 냄비에 넣었을 때 호출
    public void StartBoiling()
    {
        potImage.sprite = potInNu1;
        rhythmGamePanel.SetActive(true);

        successCount = 0;
        UpdateFireUI();

        StartRound();
    }

    void StartRound()
    {
        isPlaying = true;

        // ⭐ 바 이동 범위 설정 (중요!!)
        float halfWidth = barArea.rect.width / 2f;
        minX = -halfWidth;
        maxX = halfWidth;

        // 바 초기 위치
        movingBar.anchoredPosition =
            new Vector2(minX, movingBar.anchoredPosition.y);

        movingRight = true;

        // 노란 영역 랜덤 이동
        MoveSuccessZoneRandom();
    }

    void MoveSuccessZoneRandom()
    {
        float barWidth = barArea.rect.width;
        float zoneWidth = successZone.rect.width;

        float zoneMinX = -barWidth / 2 + zoneWidth / 2;
        float zoneMaxX = barWidth / 2 - zoneWidth / 2;

        float randomX = Random.Range(zoneMinX, zoneMaxX);

        successZone.anchoredPosition =
            new Vector2(randomX, successZone.anchoredPosition.y);
    }

    void MoveBar()
    {
        float move = barSpeed * Time.deltaTime;
        Vector2 pos = movingBar.anchoredPosition;

        pos.x += movingRight ? move : -move;

        if (pos.x >= maxX) movingRight = false;
        if (pos.x <= minX) movingRight = true;

        movingBar.anchoredPosition = pos;
    }

    void CheckSuccess()
{
    if (inputLocked) return;

    inputLocked = true;
    isPlaying = false;

    attemptCount++; // ⭐ 도전 횟수 증가

    float barX = movingBar.anchoredPosition.x;
    float zoneMin = successZone.anchoredPosition.x - successZone.rect.width / 2;
    float zoneMax = successZone.anchoredPosition.x + successZone.rect.width / 2;

    // 🎯 성공 판정
    if (barX >= zoneMin && barX <= zoneMax)
    {
        successCount++;
        UpdateFireUI();
        UpdatePotImage();
    }

    // 🎉 성공 조건
    if (successCount >= maxRounds)
    {
            FinishBoilingSuccess();   // 성공 패널
    }
    // ❌ 실패 조건 (기회 다 씀)
    else if (attemptCount >= maxRounds)
    {
        FinishBoilingFail(); // 실패 패널 또는 다음 씬
    }
    else
    {
        StartCoroutine(NextRoundDelay());
    }
}

    // 🔥 불 UI 업데이트
    void UpdateFireUI()
    {
        for (int i = 0; i < progressSlots.Length; i++)
        {
            progressSlots[i].gameObject.SetActive(i < successCount);
        }
    }

    // 🍲 냄비 이미지 단계 변경
    void UpdatePotImage()
    {
        if (successCount >= 5)
        {
            potImage.sprite = potInNu2;
        }
        else if (successCount >= 3)
        {
            potImage.sprite = nuddleLeft;
        }
        else if (successCount >= 1)
        {
            potImage.sprite = potInNu1;
        }
    }


    // 🎉 최종 성공
    void FinishBoilingSuccess()
    {
        isPlaying = false;
        inputLocked = true;

        // 리듬게임 화면은 그대로 두거나, 필요하면 꺼도 됨
        // rhythmGamePanel.SetActive(false);

        BoilSuccessPanel.SetActive(true);

        Time.timeScale = 0f; // ⭐ 여기서 "멈춤"
    }
    void FinishBoilingFail()
    {
        isPlaying = false;
        inputLocked = true;

        BoilFailPanel.SetActive(true);   // 실패 패널
                                     // rhythmGamePanel.SetActive(false);

        Time.timeScale = 0f;         // 게임 멈춤
    }


    // ⏱ 다음 라운드 딜레이
    IEnumerator NextRoundDelay()
    {
        inputLocked = true;
        yield return new WaitForSeconds(0.15f); // 체감 좋음
        inputLocked = false;
        StartRound();
    }
}
