using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    Animator Ani;

    private void Start()
    {
        WaveSpawner.OnWaveStart += StartPortalling;
        WaveSpawner.OnCompletedSummons += EndPortalling;
        gameObject.SetActive(false);
    }

    void StartPortalling()
    {
        gameObject.SetActive(true);
        Ani.SetTrigger("StartAgain");
    }

    void EndPortalling()
    {
        gameObject.SetActive(false);
    }


    private void OnDestroy()
    {
        WaveSpawner.OnWaveStart -= StartPortalling;
        WaveSpawner.OnCompletedSummons -= EndPortalling;
    }
}
