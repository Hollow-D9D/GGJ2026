using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveMask : MonoBehaviour
{
    public event Action MaskEnabled;
    public event Action MaskDisabled;
    
    private bool isMaskOn = false;

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            toggleMask();
        }
    }

    void toggleMask ()
    {
       isMaskOn = !isMaskOn;

       if (isMaskOn)
       {
        MaskEnabled?.Invoke();
       }
       else
       {
        MaskDisabled?.Invoke();
       }
    }
}
