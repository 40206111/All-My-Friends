using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHub : EntityHub
{

    protected override void Start()
    {
        base.Start();
        PBlocker = new ProjectileBlocker(this, ~Faction);
    }
}
