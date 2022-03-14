using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitHub : EntityHub
{
    protected override void StartExtras()
    {
        base.StartExtras();
        PBlocker = new ProjectileBlocker(this, ~Faction);

        Mover.Speed *= 4f;
        Mover.Accel *= 4f;
    }
}
