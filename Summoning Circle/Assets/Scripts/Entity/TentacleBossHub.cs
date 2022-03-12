using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleBossHub : EntityHub
{
    protected override void StartExtras()
    {
        base.StartExtras();

        Caster.Data = new ProjectileData(3, 2, 20, 4, Faction, eFaction.all);
    }

}
