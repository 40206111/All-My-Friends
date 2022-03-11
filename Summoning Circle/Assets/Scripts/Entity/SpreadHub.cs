using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadHub : EntityHub
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        Caster.Data = new ProjectileData(10, 2, 10, 3, Faction, ~Faction);
    }
}
