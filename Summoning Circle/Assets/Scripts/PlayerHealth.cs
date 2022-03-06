using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthPool
{
    public float DamageIFrames = 0.5f;
    public float IFrameRemaining = 0f;

    public override void Damage(int val)
    {
        if (IFrameRemaining <= 0f)
        {
            base.Damage(val);
            IFrameRemaining = DamageIFrames;
        }
    }

    public void HealthUpdate()
    {
        if (IFrameRemaining > 0f)
        {
            IFrameRemaining -= Time.deltaTime;
        }
    }

    public override void Die()
    {
        Object.Destroy(Hub.gameObject);
    }
}
