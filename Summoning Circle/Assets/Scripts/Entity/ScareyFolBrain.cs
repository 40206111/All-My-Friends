using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareyFolBrain : FollowerBrain
{
    public ScareyFolBrain(EntityHub hub) : base(hub)
    {
        hub.OnTriggerEnter += OnTriggerEnter;
        hub.OnTriggerExit += OnTriggerExit;
    }

    public override void Destroy()
    {
        base.Destroy();
        Hub.OnTriggerEnter -= OnTriggerEnter;
        Hub.OnTriggerExit -= OnTriggerExit;
    }

    public eEntityActions Action = eEntityActions.idle;

    float IdleElapsed = 0f;
    float IdleTime = 5f;
    float ScareElapsed = 0f;
    float ScareTime = 3f;

    List<EntityHub> ScardyCats = new List<EntityHub>();
    List<EntityHub> ScardyCatsAfterAttack = new List<EntityHub>();

    protected override void ActionUpdate()
    {
        if (Action == eEntityActions.idle)
        {
            IdleElapsed += Time.deltaTime;

            if (IdleElapsed >= IdleTime)
            {
                ListScareState(true);
                ScardyCatsAfterAttack = new List<EntityHub>(ScardyCats);
                Action = eEntityActions.attacking;
                ScareElapsed = 0f;
            }
        }
        else if (Action == eEntityActions.attacking)
        {
            Hub.GetComponentInChildren<AnimateEntityEffects>().DisplayDamaged(0.1f); // ~~~

            ScareElapsed += Time.deltaTime;
            if (ScareElapsed >= ScareTime)
            {
                ListScareState(false);
                ScardyCats = ScardyCatsAfterAttack;
                Action = eEntityActions.idle;
                IdleElapsed = 0f;
            }
        }
    }

    protected void OnTriggerEnter(Collider2D collider)
    {
        EntityHub eh = collider.GetComponent<EntityHub>();
        if (eh != null)
        {
            if (eh.Mover != null && eh.Faction != Hub.Faction)
            {
                bool wasNew = ScardyCats.AddIfAbsent(eh);
                if (Action == eEntityActions.attacking)
                {
                    if (wasNew)
                    {
                        eh.SetFreeze(true);
                    }
                    ScardyCatsAfterAttack.AddIfAbsent(eh);
                }
            }
        }
    }

    protected void OnTriggerExit(Collider2D collider)
    {
        EntityHub eh = collider.GetComponent<EntityHub>();
        if (eh != null)
        {
            if (Action == eEntityActions.attacking)
            {
                ScardyCatsAfterAttack.Remove(eh);
            }
            else
            {
                ScardyCats.Remove(eh);
            }
        }
    }

    protected void ListScareState(bool state)
    {
        foreach (EntityHub eh in ScardyCats)
        {
            eh.SetFreeze(state);
        }
    }
}
