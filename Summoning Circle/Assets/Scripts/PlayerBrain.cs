using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : EntityBrain
{

    public override void BrainUpdate()
    {
        base.BrainUpdate();
        // ---- Movement ----
        Vector2 moveDir = Vector2.zero;
        moveDir.x = Input.GetAxisRaw("HorizontalMove");
        moveDir.y = Input.GetAxisRaw("VerticalMove");
        if (moveDir.sqrMagnitude > 1)
        {
            moveDir = moveDir.normalized;
        }
        (Hub as PlayerHub).Mover.MoveVector = moveDir;

        // ---- Firing ----
    }
}
