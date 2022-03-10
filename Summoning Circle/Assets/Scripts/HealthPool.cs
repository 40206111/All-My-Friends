using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthPool 
{
    public EntityHub Hub;

    public int MaxHealth = 1;
    public int CurrentHealth = 1;

    public virtual void Damage(int val)
    {
        CurrentHealth -= val;
        if(CurrentHealth <= 0)
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

    public virtual void HealthUpdate() { }

    public virtual void Die()
    {
        Hub.Die();
    }
}
