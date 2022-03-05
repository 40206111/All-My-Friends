using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHub : EntityHub
{
    public ushort FrozenMovement = 0;

    public EntityMover Mover;
    public EntityProjCaster Caster;
    public PlayerBrain Brain;

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

    // Update is called once per frame
    protected override void Update()
    {
        Brain.BrainUpdate();
        Mover.MoveUpdate();
        Caster.CastUpdate();
    }
}
