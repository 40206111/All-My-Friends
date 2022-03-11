using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHub : EntityHub
{

    // Start is called before the first frame update
    protected override void Start()
    {
        Faction = eFaction.player;

        Mover = new EntityMover(this);
        Caster = new EntityProjCaster(this);
        Brain = new PlayerBrain(this);
        Health = new PlayerHealth(this);

        Health.MaxHealth = 6;
        Health.CurrentHealth = 6;

        Caster.Data = new ProjectileData(10, 2, 5, 6, Faction, ~Faction);
    }
}
