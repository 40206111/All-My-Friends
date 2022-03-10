using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHub : EntityHub
{

    protected override void Start()
    {
        Mover = new EntityMover();
        Brain = new BounceBrain();

        Mover.Body = GetComponent<Rigidbody2D>();
        Mover.Hub = this;

        Brain.Hub = this;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        ((BounceBrain)Brain).OnCollisionEnter2D(collision);
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        ((BounceBrain)Brain).OnCollisionExit2D(collision);
    }
}
