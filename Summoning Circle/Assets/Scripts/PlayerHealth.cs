using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthPool
{
    public float DamageIFrames = 0.5f;
    public float IFrameRemaining = 0f;

    public PlayerHealth(EntityHub hub) : base(hub) { }

    public override void Damage(int val)
    {
        if (IFrameRemaining <= 0f)
        {
            base.Damage(1);
            IFrameRemaining = DamageIFrames;
        }
    }

    public override void HealthUpdate()
    {
        if (IFrameRemaining > 0f)
        {
            IFrameRemaining -= Time.deltaTime;
        }
    }
}
