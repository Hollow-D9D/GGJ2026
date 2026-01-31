using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMask : MonoBehaviour
{
    public event Action<bool> MaskToggled;
    
    private bool isMaskOn = false;

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            toggleMask();
        }
    }

    void toggleMask()
    {
        isMaskOn = !isMaskOn;

        MaskToggled.Invoke(isMaskOn);
    }
}
