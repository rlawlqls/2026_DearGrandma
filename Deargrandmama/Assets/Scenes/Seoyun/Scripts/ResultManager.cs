using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public Image backgroundImage; // 배경 Image 컴포넌트
    public TextMeshProUGUI subtitleText; // 자막 TextMeshPro 컴포넌트

    // 에디터에서 넣을 데이터 리스트
    public Sprite[] resultImages;
    [TextArea] public string[] englishSubtitles;

    // 특정 결과 화면을 보여주는 함수 (0~5번)
    public void ShowResult(int index)
    {
        backgroundImage.sprite = resultImages[index];
        subtitleText.text = englishSubtitles[index];
    }
    public void GoToNextScene()
    {
        // "NextSceneName" 자리에 실제 이동할 씬 이름을 넣으세요.
        // 예: 할머니 대화 씬 이름이 "GrandmaTalk"라면 그 이름을 적어야 합니다.
        SceneManager.LoadScene("NextSceneName");
    }

    void Update()
    {
        // 숫자 키 1~6을 누르면 각 결과 화면이 바뀜 (테스트용)
        if (Input.GetKeyDown(KeyCode.Alpha1)) ShowResult(0); // a
        if (Input.GetKeyDown(KeyCode.Alpha2)) ShowResult(1); // e
        if (Input.GetKeyDown(KeyCode.Alpha3)) ShowResult(2); // c
        if (Input.GetKeyDown(KeyCode.Alpha4)) ShowResult(3); // d
        if (Input.GetKeyDown(KeyCode.Alpha5)) ShowResult(4); // f
        if (Input.GetKeyDown(KeyCode.Alpha6)) ShowResult(5); // b
    }
}