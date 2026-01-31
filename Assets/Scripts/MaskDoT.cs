using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDoT : MonoBehaviour
{
    [SerializeField] private ToggleMask maskState;
    [SerializeField] private PlayerHealth health;

    [SerializeField] private int damagePerTick = 5;
    [SerializeField] private float tickInterval = 1f;

    private Coroutine dotCoroutine;

    void OnEnable()
    {
        maskState.MaskToggled += ToggleDot;
    }

    void OnDisable()
    {
        maskState.MaskToggled -= ToggleDot;
    }

    void ToggleDot(bool isMaskOn)
    {
        if (isMaskOn)
        {
            if (dotCoroutine == null)
            {
                dotCoroutine = StartCoroutine(DotLoop());
            }
        }
        else
        {
            if (dotCoroutine != null)
            {
                StopCoroutine(dotCoroutine);
                dotCoroutine = null;
            }
        }
        
    }

    IEnumerator DotLoop()
    {
        while (true)
        {
            health.DealDamage(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
