using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaltTimeoutNextButton : MonoBehaviour
{
    public void OnClickNext()
    {
        Debug.Log(" 타임아웃 Next 버튼 클릭됨");

        Time.timeScale = 1f; // 혹시 멈춘 상태 대비
        SceneManager.LoadScene("NuddleMix");
    }
}
