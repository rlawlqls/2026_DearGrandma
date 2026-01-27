using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class toolmanager : MonoBehaviour
{
    [SerializeField] private GameObject knifeCursor;        // KnifeCursor 오브젝트
    [SerializeField] private KnifeCursorFollow knifeFollow; // KnifeCursorFollow 컴포넌트(없으면 null 가능)

    private bool knifeEquipped = false;

    private void Awake()
    {
        // 시작은 내려놓은 상태
        SetKnife(false);
    }

    public void ToggleKnife()
    {
        SetKnife(!knifeEquipped);
    }

    private void SetKnife(bool equip)
    {
        knifeEquipped = equip;

        if (knifeCursor != null)
            knifeCursor.SetActive(equip);

        if (knifeFollow != null)
            knifeFollow.followOn = equip;

        // 커서 처리(원하면 유지)
        Cursor.visible = !equip;
    }
    public bool IsKnifeEquipped()
{
    return knifeCursor != null && knifeCursor.activeSelf;
}
}




