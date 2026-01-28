using UnityEngine;
using UnityEngine.SceneManagement;

public class NoodleNextButton : MonoBehaviour
{
    public void OnClickNextGame()
    {
        Debug.LogWarning("🟡 Next 버튼 클릭됨 - 씬 이동 시도");

        Time.timeScale = 1f;

        SceneManager.LoadScene("Spaghetti");
    }
}
