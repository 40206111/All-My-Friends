using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDashHub : EnemyHub
{
    protected override void StartExtras()
    {
        Mover.Speed = 3f;

        Health.MaxHealth = Health.CurrentHealth = Health.MaxHealth / 2;
    }
}
