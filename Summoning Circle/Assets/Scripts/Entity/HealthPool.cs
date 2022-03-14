using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthPool
{
    public EntityHub Hub;

    public int MaxHealth = 1;
    public int CurrentHealth = 1;

    public HealthPool(EntityHub hub)
    {
        Hub = hub;
    }
    public virtual void Destroy() { }

    public virtual void Damage(int val)
    {
        CurrentHealth -= val;
        CallAnimateDamage();
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Heal(int val)
    {
        CurrentHealth += val;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
    protected virtual void CallAnimateDamage()
    {
        Hub.GetComponentInChildren<AnimateEntityEffects>()?.DisplayDamaged(0.2f);
    }

    public virtual void HealthUpdate() { }

    public virtual void Die()
    {
        Hub.Die();
    }

    public virtual void IncreaseMaxHealth(int increase, bool healIncrease = true)
    {
        MaxHealth += increase;
        if (healIncrease)
        {
            Heal(increase);
        }
    }
}
