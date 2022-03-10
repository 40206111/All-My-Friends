using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHub : EntityHub
{

    public Transform Projectile;

    // Start is called before the first frame update
    protected override void Start()
    {
        Mover = new EntityMover(this);
        Caster = new EntityProjCaster(this);
        Brain = new PlayerBrain(this);
        Health = new PlayerHealth(this);

        Mover.Body = GetComponent<Rigidbody2D>();

        Health.MaxHealth = 6;
        Health.CurrentHealth = 6;
    }
}
