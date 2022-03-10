using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitHub : EntityHub
{
    protected override void Start()
    {
        Mover = new EntityMover(this);
        Brain = new OrbitBrain(this);
        PBlocker = new ProjectileBlocker(this, eFaction.enemy | eFaction.neutral);

        Mover.Speed *= 4f;
        Mover.Accel *= 4f;
    }
}
