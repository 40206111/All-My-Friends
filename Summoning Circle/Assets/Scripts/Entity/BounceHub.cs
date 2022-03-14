using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHub : EntityHub
{

    protected override void StartExtras()
    {
        base.StartExtras();
        PBlocker = new ProjectileBlocker(this, ~Faction);
    }
}
