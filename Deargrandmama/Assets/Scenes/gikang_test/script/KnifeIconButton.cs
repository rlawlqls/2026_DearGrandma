using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeIconButton : MonoBehaviour
{
    [SerializeField] private toolmanager manager;

    private void OnMouseDown()
    {
        if (manager == null) return;
        manager.ToggleKnife();
    }

}

