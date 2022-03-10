using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHub : EntityHub
{

    protected override void Start()
    {
        Mover = new EntityMover(this);
        Brain = new BounceBrain(this);

        Mover.Body = GetComponent<Rigidbody2D>();
    }
}
