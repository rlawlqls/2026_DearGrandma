using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro를 쓰니까 꼭 추가해야 해!
using System.Collections;

public class RecipeManager : MonoBehaviour
{
    // 계층 구조에 있는 버튼 8개를 여기에 연결
    public Button[] stepButtons;
    private Coroutine blinkCoroutine; // 현재 실행 중인 반짝임 코루틴 저장용

    private static bool isFirstEntry = true;
    void Start()
    {
        if (isFirstEntry)
        {
            PlayerPrefs.SetInt("RecipeStep", 0);
            PlayerPrefs.Save();
            isFirstEntry = false; // 이후 씬 전환으로 다시 돌아올 때는 이 블록을 실행하지 않음
            Debug.Log("게임 시작: 진행도 초기화됨");
        }

        // 1. 저장된 진행 단계 불러오기 (없으면 0)
        int currentStep = PlayerPrefs.GetInt("RecipeStep", 0);

        // 2. 불러온 단계로 UI 업데이트 (반짝임 포함)
        UpdateRecipeProgress(currentStep);

        // 3. 버튼 클릭 이벤트들 연결
        if (stepButtons.Length >= 2)
        {
            // 0번 버튼 (소시지)
            stepButtons[0].onClick.RemoveAllListeners(); // 기존 설정 초기화
            stepButtons[0].onClick.AddListener(() => OnStepClick("SoursageSlicing"));

            // 1번 버튼 (양파)
            stepButtons[1].onClick.RemoveAllListeners();
            stepButtons[1].onClick.AddListener(() => OnStepClick("onionslice"));

            // 2번 버튼 (캔따기)
            stepButtons[2].onClick.RemoveAllListeners();
            stepButtons[2].onClick.AddListener(() => OnStepClick("CanOpen"));

            // 3번 버튼 (면삶기)
            stepButtons[2].onClick.RemoveAllListeners();
            stepButtons[2].onClick.AddListener(() => OnStepClick("NuddleMix"));

        }
    }

    public void UpdateRecipeProgress(int currentStep)
    {
        // 새로 업데이트하기 전에 기존에 돌고 있던 반짝임은 멈춤
        if (blinkCoroutine != null) StopCoroutine(blinkCoroutine);

        for (int i = 0; i < stepButtons.Length; i++)
        {
            // 현재 단계만 클릭 가능하게 설정
            bool isActive = (i == currentStep);
            stepButtons[i].interactable = isActive;

            // 버튼 자식에 있는 TextMeshPro 찾기
            TextMeshProUGUI btnText = stepButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (btnText != null)
            {
                if (isActive)
                {
                    // 현재 단계라면? 반짝이기 시작!
                    blinkCoroutine = StartCoroutine(BlinkText(btnText));
                }
                else
                {
                    // 다른 단계라면? 반투명하고 어둡게 고정
                    btnText.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                }
            }
        }
    }

    // 텍스트를 반짝이게 만드는 코루틴
    IEnumerator BlinkText(TextMeshProUGUI text)
    {
        // 진한 파란색 (R: 0, G: 0.2, B: 0.8 정도로 설정하면 아주 진하고 선명해)
        Color targetColor = new Color(0.0f, 0.2f, 0.8f);

        while (true)
        {
            // 알파값(투명도) 범위를 0.5 ~ 1.0으로 해서 파란색이 흐리멍덩해지지 않게 함
            float alpha = Mathf.PingPong(Time.time * 2.0f, 0.5f) + 0.5f;

            // 설정한 진한 파란색에 실시간 알파값 적용
            text.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
            yield return null;
        }
    }

    public void OnStepClick(string sceneName)
    {
        // 지정된 이름의 씬으로 이동
        SceneManager.LoadScene(sceneName);
    }
}