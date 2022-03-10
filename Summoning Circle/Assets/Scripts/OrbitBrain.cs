using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitBrain : EntityBrain
{
    public float OrbitPeriodScalar;
    public float OrbitProgress = 0.0f;

    public static PlayerHub Player;

    public OrbitBrain(EntityHub hub) : base(hub)
    {
        SetOrbitPeriod(3.0f);
    }

    public override void BrainUpdate()
    {
        if (Player == null)
        {
            Player = Object.FindObjectOfType<PlayerHub>();
            if (Player == null)
            {
                return;
            }
        }

        OrbitProgress += Time.deltaTime * OrbitPeriodScalar;

        Vector2 targetPos = (Vector2)Player.transform.position + Vector2.up.Rotate(OrbitProgress * MathUtils.Tau);
        Vector2 dirToTarget = targetPos - (Vector2)Hub.transform.position;
        Hub.Mover.MoveVector = dirToTarget.MaxLength(1f);
    }

    /// <summary>
    /// Set orbit time in seconds
    /// </summary>
    /// <param name="val"></param>
    public void SetOrbitPeriod(float val)
    {
        OrbitPeriodScalar = 1.0f / val;
    }
}
