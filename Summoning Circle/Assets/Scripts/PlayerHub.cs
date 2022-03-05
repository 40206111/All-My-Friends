using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHub : EntityHub
{

    public Transform Projectile;

    // Start is called before the first frame update
    protected override void Start()
    {
        Mover = new EntityMover();
        Caster = new EntityProjCaster();
        Brain = new PlayerBrain();

        Mover.Body = GetComponent<Rigidbody2D>();
        Mover.Hub = this;

        Caster.Hub = this;

        Brain.Hub = this;
    }
}
