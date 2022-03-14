using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : EntityBrain
{
    public Vector2 Target = Vector2.zero;

    public bool TargetIsPlayer = true;

    public EnemyBrain(EntityHub hub) : base(hub) { }

    public override void BrainUpdate()
    {

        if (TargetIsPlayer)
        {
            if (PlayerHub.Instance == null)
            {
                return;
            }
            Target = PlayerHub.Instance.transform.position;
        }

        Vector2 toPlayer = (Target - (Vector2)Hub.transform.position).normalized;

        Hub.Mover.MoveVector = toPlayer;

        Hub.Caster.CastDirection = toPlayer;
    }
}
