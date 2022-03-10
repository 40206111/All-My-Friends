using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthPool
{
    public EnemyHealth(EntityHub hub) : base(hub)
    {
        MaxHealth = 50;
        CurrentHealth = 50;
    }
}
