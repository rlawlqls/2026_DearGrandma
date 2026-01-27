using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLineManager : MonoBehaviour
{
    private guideline[] lines;
    private int currentIndex = 0;

    public guideline CurrentLine
    {
        get
        {
            if (lines == null || lines.Length == 0) return null;
            if (currentIndex < 0 || currentIndex >= lines.Length) return null;
            return lines[currentIndex];
        }
    }

    private void Awake()
    {
        // 자식 순서대로 자동 수집 (왼→오)
        lines = GetComponentsInChildren<guideline>(true);

        SetActiveOnly(currentIndex);
    }

    public void Advance()
    {
        currentIndex++;

        if (currentIndex >= lines.Length)
        {
            // 전부 컷 완료
            SetActiveOnly(-1);
            Debug.Log("All guide lines completed!");
            return;
        }

        SetActiveOnly(currentIndex);
    }

    private void SetActiveOnly(int activeIndex)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            bool on = (i == activeIndex);
            lines[i].gameObject.SetActive(on);
        }
    }
}
