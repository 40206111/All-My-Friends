
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SummoningSigil
{
    public eRoundSigil Sigil = eRoundSigil.none;
    public Sprite Small;
    public Sprite Large;
}

public enum eRoundSigil { none = -1, Goblin, Item, Skull }

public class SummoningRing : MonoBehaviour
{
    public List<Transform> Rings;
    public SpriteRenderer LargeSigil;
    public SummoningSigil GoblinSigil;
    public SummoningSigil ItemSigil;
    public SummoningSigil SkullSigil;

    public Transform Ring;

    private SummoningSigil Selected = null;

    private void Start()
    {
        SigilCircle.OnPlayerEnter += PlayerSigilEvent;
        StartSummonCircle.OnPlayerEnter += StartSummon;
        WaveSpawner.OnBossEnd += AfterBoss;

        SetCircleSigils();
    }

    private void AfterBoss()
    {
        SetCircleSigils();
        gameObject.SetActive(true);
    }

    private void PlayerSigilEvent(SigilCircle sigil)
    {
        LargeSigil.sprite = sigil.Sigil.Large;
        Selected = sigil.Sigil;
    }

    private void StartSummon()
    {
        if (Selected != null)
        {
            WaveSpawner.Instance.StartSummon(Selected.Sigil);
            Destroy(Ring.gameObject);
            Ring = Instantiate(Rings[Random.Range(1, Rings.Count)], transform);
            Ring.localPosition = Vector3.zero;
            LargeSigil.sprite = null;
            Selected = null;
            gameObject.SetActive(false);
        }
    }

    private void SetCircleSigils()
    {
        SigilCircle[] circles = Ring.GetComponentsInChildren<SigilCircle>();
        switch (circles.Length)
        {
            case 3:
                circles[2].SetSigil(SkullSigil);
                goto case 2;
            case 2:
                circles[1].SetSigil(ItemSigil);
                goto case 1;
            case 1:
                circles[0].SetSigil(GoblinSigil);
                break;
            default:
                if (circles.Length > 3)
                {
                    goto case 3;
                }
                else
                {
                    break;
                }
        }
    }
}
