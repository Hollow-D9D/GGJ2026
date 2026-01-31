using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDoT : MonoBehaviour
{
    [SerializeField] private RemoveMask maskState;
    [SerializeField] private PlayerHealth health;

    [SerializeField] private float damagePerTick = 5f;
    [SerializeField] private float tickInterval = 1f;

    private Coroutine dotCoroutine;

    void OnEnable()
    {
        maskState.MaskEnabled += StartDoT;
        maskState.MaskDisabled += StopDoT;
    }

    void OnDisable()
    {
        maskState.MaskEnabled -= StartDoT;
        maskState.MaskDisabled -= StopDoT;
    }

    void StartDoT()
    {
        if (dotCoroutine == null)
        {
            dotCoroutine = StartCoroutine(DotLoop());
        }
    }

    void StopDoT()
    {
        if (dotCoroutine != null)
        {
            StopCoroutine(dotCoroutine);
            dotCoroutine = null;
        }
    }

    IEnumerator DotLoop()
    {
        while (true)
        {
            health.TakeDamage(damagePerTick);
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
