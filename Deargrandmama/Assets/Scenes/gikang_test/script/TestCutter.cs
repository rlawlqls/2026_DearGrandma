using UnityEngine;

public class TestCutter : MonoBehaviour
{
    [SerializeField] private CuttableObject target;
    [SerializeField] private KeyCode cutKey = KeyCode.Space;

    private void Update()
    {
        if (Input.GetKeyDown(cutKey))
        {
            if (target == null)
            {
                Debug.LogError("TestCutter: target is null!");
                return;
            }

            target.ApplyOneCut();
            Debug.Log("Space pressed -> ApplyOneCut()");
        }
    }
}
