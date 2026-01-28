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
    public GameObject successPanel;

    public float barSpeed = 400f;
    public int maxRounds = 5;

    private int successCount = 0;
    private bool isPlaying = false;
    private bool movingRight = true;
    private float minX, maxX;

    private bool inputLocked = false;

    void Start()
    {
        rhythmGamePanel.SetActive(false);
        successPanel.SetActive(false);

        foreach (var slot in progressSlots)
            slot.color = defaultColor;
    }

    void Update()
    {
        if (!isPlaying || inputLocked) return;

        MoveBar();

        if (Input.GetKeyDown(KeyCode.Space))
            CheckSuccess();
    }


    // 🍝 면을 냄비에 넣었을 때 호출
    public void StartBoiling()
    {
        potImage.sprite = potInNu1; // ⭐ 1단계 냄비
        rhythmGamePanel.SetActive(true);

        successCount = 0;
        UpdateFireUI();
        StartRound();
    }

    void StartRound()
    {
        isPlaying = true;

        // 막대 초기화
        movingBar.anchoredPosition =
            new Vector2(-barArea.rect.width / 2, movingBar.anchoredPosition.y);
        movingRight = true;

        // ⭐️ 여기!
        MoveSuccessZoneRandom();
    }
    void MoveSuccessZoneRandom()
    {
        float barWidth = barArea.rect.width;
        float zoneWidth = successZone.rect.width;

        // BarArea 안에서 벗어나지 않게 X 범위 계산
        float minX = -barWidth / 2 + zoneWidth / 2;
        float maxX = barWidth / 2 - zoneWidth / 2;

        float randomX = Random.Range(minX, maxX);

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
        float barX = movingBar.anchoredPosition.x;

        float zoneMin = successZone.anchoredPosition.x - successZone.rect.width / 2;
        float zoneMax = successZone.anchoredPosition.x + successZone.rect.width / 2;

        if (barX >= zoneMin && barX <= zoneMax)
        {
            successCount++;

            UpdateFireUI();   // 🔥 불 켜기
            UpdatePotImage(); // 냄비 이미지 변경

            if (successCount >= maxRounds)
            {
                FinishBoiling();
            }
            else
            {
                StartRound(); // 다음 도전
            }
        }
    }

    // 🟢 성공 UI 업데이트
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
        if (successCount == 3)
            potImage.sprite = nuddleLeft;

        if (successCount == 5)
            potImage.sprite = potInNu2;
    }

    // 🎉 최종 성공
    void FinishBoiling()
    {
        isPlaying = false;
        rhythmGamePanel.SetActive(false);
        successPanel.SetActive(true);
    }
    IEnumerator NextRoundDelay()
    {
        inputLocked = true;
        yield return new WaitForSeconds(0.15f); // 0.1~0.2 추천
        inputLocked = false;
        StartRound();
    }

}
