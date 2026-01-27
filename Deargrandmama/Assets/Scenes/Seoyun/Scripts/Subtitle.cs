using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class Subtitle : MonoBehaviour
{
    public string nicknameInput;
    public TMP_Text dialogueText;
    public float typingSpeed = 0.05f;
    public GameObject NextButton;

    private const string NICKNAME_KEY = "PLAYER_NICKNAME";

    void Start()
    {
        if (NextButton != null)
        {
            NextButton.SetActive(false);
        }
        if (PlayerPrefs.HasKey(NICKNAME_KEY))
        {
            nicknameInput = PlayerPrefs.GetString(NICKNAME_KEY);
        }
        string nickname = PlayerPrefs.GetString(NICKNAME_KEY, "Player");

    

        // 2. 닉네임을 포함한 문장 만들기
        string fullSentence = $"{nickname} I want to eat some nostalgic food. Could you make it for me?";

        // 3. 화면에 출력
        dialogueText.text = fullSentence;
        StartCoroutine(TypeText(fullSentence));
    }
    IEnumerator TypeText(string line)
    {
        dialogueText.text = ""; // 먼저 자막창을 비웁니다.

        // 한 글자씩 반복해서 출력
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter; // 한 글자 추가
            yield return new WaitForSeconds(typingSpeed); // 설정한 시간만큼 대기
        }

        if(NextButton !=null)
        {
            NextButton.SetActive(true);
        }
        //public static string GetNickname()
        //{
        //return PlayerPrefs.GetString(NICKNAME_KEY, "아가"); // 기본값
        // }
        void Update()
        {

        }
    }
}
