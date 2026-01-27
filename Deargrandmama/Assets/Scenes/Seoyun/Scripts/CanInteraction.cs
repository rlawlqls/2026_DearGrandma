using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanInteraction : MonoBehaviour
{
    private bool isHoldingOpener = false;
    public GameObject openCanImage;      // 다 열린 캔 이미지 (비활성화 해둠)
    public GameObject closedCanImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHoldingOpener)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Can") && Input.GetMouseButton(0))
        {
            // 여기에 가이드라인을 따라 원을 그리는 로직이나 타이머 추가
            Debug.Log("캔을 따는 중...");

            // 예시: 특정 조건(타이머 종료 등) 충돌 시 캔 교체
            // OpenCan(); 
        }
    }

    public void OpenCan()
    {
        closedCanImage.SetActive(false);
        openCanImage.SetActive(true);
        Debug.Log("캔이 열렸습니다! 소스가 보입니다.");
    }
}

