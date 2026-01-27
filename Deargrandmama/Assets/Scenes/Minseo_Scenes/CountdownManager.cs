using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // 인스펙터에서 CountdownText 연결
    public GameObject gameLogic; // 게임 시작을 제어할 오브젝트 (스크립트)

    void Start()
    {
        // 처음엔 게임 로직을 꺼두기
        if (gameLogic != null) gameLogic.SetActive(false);

        // 카운트다운 코루틴 시작
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        int count = 3;

        while (count > 0)
        {
            countdownText.text = count.ToString(); // 숫자 표시
            yield return new WaitForSeconds(1.0f); // 1초 대기
            count--;
        }

        countdownText.text = "GO!"; // 시작 알림
        yield return new WaitForSeconds(1.0f);

        countdownText.gameObject.SetActive(false); // 텍스트 숨기기

        // 게임 시작! (꺼뒀던 게임 로직 켜기)
        if (gameLogic != null) gameLogic.SetActive(true);

        Debug.Log("게임 시작됨!");
    }
}