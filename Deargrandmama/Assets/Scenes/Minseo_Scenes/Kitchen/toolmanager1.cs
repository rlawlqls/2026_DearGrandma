using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class toolmanager1 : MonoBehaviour
{
    [SerializeField] private GameObject penchCursor;        // KnifeCursor 오브젝트
    [SerializeField] private PenchCursorFollow penchFollow; // KnifeCursorFollow 컴포넌트(없으면 null 가능)

    private bool penchEquipped = false;

    private void Awake()
    {
        // 시작은 내려놓은 상태
        SetPench(false);
    }

    public void ToggleKnife()
    {
        SetPench(!penchEquipped);
    }

    private void SetPench(bool equip)
    {
        penchEquipped = equip;

        if (penchCursor != null)
            penchCursor.SetActive(equip);

        if (penchFollow != null)
            penchFollow.followOn = equip;

        // 커서 처리(원하면 유지)
        Cursor.visible = !equip;
    }
    public bool IsKnifeEquipped()
{
    return penchCursor != null && penchCursor.activeSelf;
}
}




