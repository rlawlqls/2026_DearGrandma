using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // �ν����Ϳ��� CountdownText ����
    public GameObject gameLogic; // ���� ������ ������ ������Ʈ (��ũ��Ʈ)

    void Start()
    {
        // ó���� ���� ������ ���α�
        if (gameLogic != null) gameLogic.SetActive(false);

        // ī��Ʈ�ٿ� �ڷ�ƾ ����
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        int count = 3;

        while (count > 0)
        {
            countdownText.text = count.ToString(); // ���� ǥ��
            yield return new WaitForSeconds(1.0f); // 1�� ���
            count--;
        }

        countdownText.text = "GO!"; // ���� �˸�
        yield return new WaitForSeconds(1.0f);

        countdownText.gameObject.SetActive(false); // �ؽ�Ʈ �����

        // ���� ����! (���״� ���� ���� �ѱ�)
        if (gameLogic != null) gameLogic.SetActive(true);

        Debug.Log("���� ���۵�!");
    }
}