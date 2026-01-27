using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDropdown : MonoBehaviour
{
    private static MenuDropdown instance;

    public GameObject MenuPanel;
    public GameObject PauseOverlay;

    public Image PauseButtonImage;   // 🔥 버튼 이미지
    public Sprite PauseSprite;       // ⏸️ 일시정지 이미지
    public Sprite ResumeSprite;      // ▶️ 다시시작 이미지

    private bool IsMenuOpen = false;
    private bool IsPaused = false;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        MenuPanel.SetActive(false);
        PauseOverlay.SetActive(false);
        Time.timeScale = 1f;

        PauseButtonImage.sprite = PauseSprite; // 초기 이미지
    }

    // ☰ 메뉴 버튼
    public void ToggleMenu()
    {
        IsMenuOpen = !IsMenuOpen;
        MenuPanel.SetActive(IsMenuOpen);
    }

    // ⏸️ / ▶️ 일시정지 버튼
    public void PauseGame()
    {
        if (IsPaused)
        {
            // ▶️ 다시 시작
            Time.timeScale = 1f;
            IsPaused = false;

            PauseOverlay.SetActive(false);
            PauseButtonImage.sprite = PauseSprite;
        }
        else
        {
            // ⏸️ 일시정지
            Time.timeScale = 0f;
            IsPaused = true;

            PauseOverlay.SetActive(true);
            PauseButtonImage.sprite = ResumeSprite;
        }

        MenuPanel.SetActive(false);
        IsMenuOpen = false;
    }

    // ❌ Quit
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("게임 종료");
    }
}

//using system.collections;
//using system.collections.generic;
//using unityengine;
//using tmpro;

//public class menudropdown : monobehaviour
//{
//    private static menudropdown instance; //전역변수로 설정

//    public gameobject menupanel;
//    public gameobject pauseoverlay;            // 전체 화면 일시정지 ui
//    public textmeshprougui pausebuttontext;    // 일시정지 버튼의 텍스트

//    private bool ismenuopen = false;
//    private bool ispaused = false;

//    // 🔥 전역 메뉴 설정 (씬 전환 시 유지 + 중복 방지)
//    void awake()
//    {
//        if (instance != null)
//        {
//            destroy(gameobject); // 이미 있으면 새로 생긴 건 제거
//            return;
//        }

//        instance = this;
////        dontdestroyonload(gameobject);
//    }
//    // start is called before the first frame update
//    void start()
//    {
//        menupanel.setactive(false);
//        pauseoverlay.setactive(false);
//        time.timescale = 1f;

//        pausebuttontext.text = "pause";//일시정지 -> 초기 텍스트
//    }

//      // ☰ 버튼(지금은 m버튼)
//    public void togglemenu()
//    {
//        ismenuopen = !ismenuopen;
//        menupanel.setactive(ismenuopen);
//    }

//    //일시정지 버튼
//    public void pausegame()
//    {
//        if (ispaused)
//        {
//             // 다시 시작
//            time.timescale = 1f; //게임 재개
//            ispaused = false;

//            pauseoverlay.setactive(false); //오버레이 제거
//            pausebuttontext.text = "pause"; //텍스트 변경
//        }
//        else
//        {
//            //일시정지
//            time.timescale = 0f; //게임 전체가 멈춤
//            ispaused = true;

//            pauseoverlay.setactive(true); //오버레이 표시
//            pausebuttontext.text = "restart"; //텍스트 변경
//        }

//        menupanel.setactive(false); //메뉴 버튼 안의 패널 닫기
//        ismenuopen = false;
//    }

//    // 그만두기 버튼
//    public void quitgame()
//    {
//        time.timescale = 1f;
//        application.quit();
//        debug.log("게임 종료");
//    }

//}
