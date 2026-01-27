using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class NicknameManager : MonoBehaviour
{

    public TMP_InputField nicknameInput;

    private const string NICKNAME_KEY = "PLAYER_NICKNAME";

    void Start()
    {
        // 이미 저장된 닉네임이 있으면 불러오기
        if (PlayerPrefs.HasKey(NICKNAME_KEY))
        {
            nicknameInput.text = PlayerPrefs.GetString(NICKNAME_KEY);
        }
    }

    public void SaveNickname()
    {
        string nickname = nicknameInput.text;

        if (string.IsNullOrWhiteSpace(nickname))
        {
            Debug.Log("닉네임이 비어있음!");
            return;
        }

        PlayerPrefs.SetString(NICKNAME_KEY, nickname);
        PlayerPrefs.Save();

        Debug.Log($"닉네임 저장됨: {nickname}");

        // 👉 다음 할머니 씬으로 이동 -> 서윤 언니 파일 이름으로 바꿔야함
        SceneManager.LoadScene("Story");
    }

    // 다른 스크립트에서 가져다 쓰기용
    public static string GetNickname()
    {
        return PlayerPrefs.GetString(NICKNAME_KEY, "아가"); // 기본값
    }

    public static void ClearNickname()
    {
        PlayerPrefs.DeleteKey(NICKNAME_KEY);
        PlayerPrefs.Save();
        //마지막 화면에서 
        //NicknameManager.ClearNickname(); 사용하면 닉네임 저장되어있는 거 사라짐
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
