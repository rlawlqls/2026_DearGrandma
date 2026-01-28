using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotTrigger : MonoBehaviour
{
    public RhythmBoilController rhythmGame;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Noodle"))
        {
            rhythmGame.StartBoiling();   // ⭐ 리듬게임 시작
            Destroy(other.gameObject);  // 면 삭제
        }
    }
}
