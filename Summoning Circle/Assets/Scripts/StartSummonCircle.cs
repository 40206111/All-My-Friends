using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartSummonCircle : MonoBehaviour
{
    public static Action OnPlayerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerEnter?.Invoke();
        }
    }
}
