using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBlocker
{
    public EntityHub Hub;
    public eFaction BlockedFactions;

    public ProjectileBlocker(EntityHub hub, eFaction blocks)
    {
        Hub = hub;
        BlockedFactions = blocks;

        hub.OnTriggerEnter += OnTriggerEnter;
    }

    public void OnTriggerEnter(Collider2D collision)
    {
        Projectile proj = collision.GetComponent<Projectile>();
        if (proj != null)
        {
            if ((BlockedFactions & proj.Data.SourceFaction) != eFaction.none)
            {
                proj.Die();
            }
        }
    }
}
