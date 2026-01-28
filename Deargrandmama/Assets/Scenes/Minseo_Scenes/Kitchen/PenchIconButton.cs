using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenchIconButton : MonoBehaviour
{
    [SerializeField] private toolmanager1 manager;

    private void OnMouseDown()
    {
        if (manager == null) return;
        manager.ToggleKnife();
    }

}

