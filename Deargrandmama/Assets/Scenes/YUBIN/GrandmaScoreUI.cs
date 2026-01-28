using UnityEngine;
using UnityEngine.UI;

public class GrandmaScoreUI : MonoBehaviour
{
    public Image grandmaImage;

    public Sprite score500;
    public Sprite score400;
    public Sprite score350;
    public Sprite score0;

    public void UpdateGrandma(int score)
    {
        if (score >= 500)
        {
            grandmaImage.sprite = score500;
        }
        else if (score >= 400)
        {
            grandmaImage.sprite = score400;
        }
        else if (score >= 350)
        {
            grandmaImage.sprite = score350;
        }
        else
        {
            grandmaImage.sprite = score0;
        }
    }
}
