using UnityEngine;

public class OpenerController : MonoBehaviour
{
    private bool isHolding = false; // 현재 도구를 들고 있는지 상태

    void Update()
    {
        // 도구를 들고 있는 상태라면 마우스 좌표를 계속 따라다님
        if (isHolding)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // 2D이므로 z축 고정
            transform.position = mousePos;
        }

        // 마우스 왼쪽 버튼을 눌렀을 때의 처리
        if (Input.GetMouseButtonDown(0))
        {
            if (isHolding)
            {
                // 이미 들고 있다면 내려놓기
                isHolding = false;
            }
            else
            {
                // 들고 있지 않다면, 마우스 위치에 오프너가 있는지 확인 후 집어올리기
                CheckClickOpener();
            }
        }
    }

    void CheckClickOpener()
    {
        // 마우스 클릭 위치에 있는 오브젝트 감지
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("클릭된 물체: " + hit.collider.name); // 무엇이 클릭되었는지 콘솔에 표시
            if (hit.collider.gameObject == gameObject)
            {
                isHolding = true;
                Debug.Log("오프너를 집었습니다!");
            }
        }
        else
        {
            Debug.Log("아무것도 클릭되지 않음");
        }
    }
}
