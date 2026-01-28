using System.Collections.Generic;
using UnityEngine;

public class RandomEval : MonoBehaviour
{
    // 텍스트(string)가 아니라 실제 오브젝트(GameObject)를 담아야 합니다.
    public List<GameObject> sceneObjects;

    void Start()
    {
        // 1. 먼저 모든 오브젝트를 다 끕니다.
        foreach (GameObject obj in sceneObjects)
        {
            obj.SetActive(false);
        }

        // 2. 리스트 중에서 무작위로 하나를 고릅니다.
        if (sceneObjects.Count > 0)
        {
            int randomIndex = Random.Range(0, sceneObjects.Count);

            // 3. 고른 오브젝트만 화면에 나타나게(True) 합니다.
            sceneObjects[randomIndex].SetActive(true);

            Debug.Log("활성화된 오브젝트: " + sceneObjects[randomIndex].name);
        }
    }
}