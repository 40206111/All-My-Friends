using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : HealthPool
{
    public float DamageIFrames = 0.5f;
    public float IFrameRemaining = 0f;

    public static Action<PlayerHealth> OnHealthUpdate;

    public PlayerHealth(EntityHub hub) : base(hub)
    {
        WaveSpawner.OnBossEnd += MaxHealthAfterBoss; 
    }

    public override void Destroy()
    {
        WaveSpawner.OnBossEnd -= MaxHealthAfterBoss;
    }

    public override void Damage(int val)
    {
        if (IFrameRemaining <= 0f)
        {
            base.Damage(1);
            IFrameRemaining = DamageIFrames;
            OnHealthUpdate?.Invoke(this);
        }
    }

    public override void Heal(int val)
    {
        base.Heal(val);
        OnHealthUpdate?.Invoke(this);
    }

    protected override void CallAnimateDamage()
    {
        Hub.GetComponentInChildren<AnimateEntityEffects>().DisplayDamaged(DamageIFrames);
    }

    public override void HealthUpdate()
    {
        if (IFrameRemaining > 0f)
        {
            IFrameRemaining -= Time.deltaTime;
        }
    }

    private void MaxHealthAfterBoss()
    {
        IncreaseMaxHealth(2);
    }

    public override void IncreaseMaxHealth(int increase, bool healIncrease = true)
    {
        if(MaxHealth == 20)
        {
            if (healIncrease)
            {
                Heal(increase);
            }
            return;
        }
        base.IncreaseMaxHealth(increase, healIncrease);
    }
}
