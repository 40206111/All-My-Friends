using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDashHub : EnemyHub
{
    protected override void Start()
    {
        Faction = eFaction.enemy;

        Mover = new EntityMover(this);
        Brain = new SideDashBrain(this);
        Health = new EnemyHealth(this);

        Mover.Speed = 3f;

        Health.MaxHealth = Health.CurrentHealth = Health.MaxHealth / 2;
    }
}
