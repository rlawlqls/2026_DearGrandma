using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // TextMeshPro를 사용 중이시므로 추가

public class ResultManager : MonoBehaviour
{
    public Image backgroundImage;
    public TextMeshProUGUI subtitleText;
    public List<GameObject> resultImages; // 인스펙터의 Result Images
    public List<string> englishSubtitles; // 인스펙터의 English Subtitles

    void Start()
    {
        // 1. 모든 이미지를 일단 끕니다.
        foreach (GameObject img in resultImages)
        {
            if (img != null) img.SetActive(false);
        }

        // 2. 0부터 리스트 개수(6개) 사이의 랜덤 숫자를 하나 뽑습니다.
        if (resultImages.Count > 0 && resultImages.Count == englishSubtitles.Count)
        {
            int randomIndex = Random.Range(0, resultImages.Count);

            // 3. 뽑힌 인덱스의 이미지와 자막을 적용합니다.
            resultImages[randomIndex].SetActive(true);
            subtitleText.text = englishSubtitles[randomIndex];

            Debug.Log("오늘의 스파게티 번호: " + randomIndex);
        }
    }
}