using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SigilCircle : MonoBehaviour
{
    public static Action<SigilCircle> OnPlayerEnter;
    public GameObject Flame;

    public SummoningSigil Sigil;
    public SpriteRenderer Sprite;

    private void OnEnable()
    {
        OnPlayerEnter += PlayerEnterEvent;
    }
    private void OnDisable()
    {
        OnPlayerEnter -= PlayerEnterEvent;
    }

    public void SetSigil(SummoningSigil sigil)
    {
        Sigil = sigil;
        Sprite.sprite = sigil.Small;
    }

    private void PlayerEnterEvent(SigilCircle entered)
    {
        if(entered == this)
        {
            Flame.SetActive(true);
        }
        else
        {
            Flame.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerEnter?.Invoke(this);
        }
    }
}
