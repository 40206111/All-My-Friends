using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthPool
{
    public EnemyHealth()
    {
        MaxHealth = 50;
        CurrentHealth = 50;
    }

    public override void Die()
    {
        Object.Destroy(Hub.gameObject);
    }
}
