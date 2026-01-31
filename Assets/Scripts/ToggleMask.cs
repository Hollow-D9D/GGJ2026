using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMask : MonoBehaviour
{
    public event Action<bool> MaskToggled;
    private bool isMaskOn = false;
    private bool isAllowedToPutTheMaskOn = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mirror"))
        {
            isAllowedToPutTheMaskOn = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Mirror"))
        {
            isAllowedToPutTheMaskOn = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Mirror"))
        {
            Debug.Log("Mirror triggers now!");
        }
    }


     void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isAllowedToPutTheMaskOn) 
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
