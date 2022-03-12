using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBrain : EntityBrain
{
    public static PlayerHub Player;
    public static List<Transform> Conga = new List<Transform>();
    public int ListPos = -1;

    public float MaxDist = 1.0f;

    public FollowerBrain(EntityHub hub) : base(hub)
    {
        if (Player == null)
        {
            Player = Object.FindObjectOfType<PlayerHub>();
            Conga.Add(Player.transform);
        }
        ListPos = Conga.Count;
        Conga.Add(hub.transform);
    }

    public override void BrainUpdate()
    {
        MoveUpdate();
        ActionUpdate();
    }

    protected virtual void MoveUpdate()
    {
        Vector2 dir = Conga[ListPos - 1].position - Hub.transform.position;
        if (dir.sqrMagnitude > MaxDist * MaxDist)
        {
            dir = dir - MaxDist * dir.normalized;
        }
        else
        {
            dir = Vector2.zero;
        }
        Hub.Mover.MoveVector = dir.MaxLength(1f);
    }

    protected virtual void ActionUpdate()
    {

    }
}
