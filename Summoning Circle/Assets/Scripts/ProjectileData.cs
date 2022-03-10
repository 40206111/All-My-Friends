using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData
{
    public int Damage = 1;
    public float ShotsPerSecond = 1f;
    public float Range = 1f;
    public float Speed = 1f;
    public eFaction SourceFaction = eFaction.neutral;
    public eFaction TargetFaction = eFaction.neutral & eFaction.player & eFaction.enemy;

    public ProjectileData(int damage, float sps, float range, float speed, eFaction source, eFaction target)
    {
        Damage = damage;
        ShotsPerSecond = sps;
        Range = range;
        Speed = speed;
        SourceFaction = source;
        TargetFaction = target;
    }
}
