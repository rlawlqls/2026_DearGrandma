using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeToolSelector : MonoBehaviour
{
    [SerializeField] private KnifeCursorFollow knifeCursor; // KnifeCursorFollow가 붙은 오브젝트 참조

    private void OnMouseDown()
    {
        if (knifeCursor == null) return;

        // 토글: 클릭할 때마다 켜짐/꺼짐
        knifeCursor.SetFollow(!knifeCursor.IsFollowing);
    }
}


