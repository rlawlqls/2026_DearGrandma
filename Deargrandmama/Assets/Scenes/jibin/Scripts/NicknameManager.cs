using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    }

    // 다른 스크립트에서 가져다 쓰기용
    public static string GetNickname()
    {
        return PlayerPrefs.GetString(NICKNAME_KEY, "아가"); // 기본값
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
