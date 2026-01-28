using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 반드시 필요합니다!

public class SceneChange : MonoBehaviour
{
    public string NextScene; // 이동할 다음 씬의 이름을 인스펙터에서 적어주세요.

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 또는 화면 터치 감지
        if (Input.GetMouseButtonDown(0))
        {
            // 설정한 이름의 씬을 로드합니다.
            SceneManager.LoadScene(NextScene);
        }
    }
}