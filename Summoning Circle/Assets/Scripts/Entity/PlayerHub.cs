using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHub : EntityHub
{
    private static PlayerHub _instance;
    public static PlayerHub Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _instance = null;
    }

    // Start is called before the first frame update
    protected override void StartExtras()
    {
        base.StartExtras();

        Health.MaxHealth = 6;
        Health.CurrentHealth = 6;
        PlayerHealth.OnHealthUpdate?.Invoke(Health as PlayerHealth);

        Caster.Data = new ProjectileData(10, 2, 5, 6, Faction, ~Faction);
    }
}
