using System;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum eUnlockCriteria
{
    None,
}

[Flags]
public enum eTags
{
    None,
    Goblin = 1 << 0,
}


[Flags]
public enum eItemPools
{
    None,
    Goblin = 1 << 0,
    Challenge = 1 << 1
}

public class Item : MonoBehaviour
{
    public string Name;
    public string TagLine;

    public eTags Tags;
    public eUnlockCriteria UnlockCriteria;
    public eItemPools ItemPools;

    public Sprite PedastalIcon;

    public Color Colour = Color.white;

}
