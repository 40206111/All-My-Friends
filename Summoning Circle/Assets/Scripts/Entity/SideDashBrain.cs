using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDashBrain : EntityBrain
{
    public static PlayerHub Player;

    public eEntityActions Action = eEntityActions.none;

    private float IdleElapsed = 0f;
    private float IdleTime = 2f;
    private float MoveElapsed = 0f;
    private float MoveTime = 1f;

    private Vector2 TravelDir = Vector2.zero;

    public float MaxAngle = 100f;

    public SideDashBrain(EntityHub hub) : base(hub)
    {
        Player = Object.FindObjectOfType<PlayerHub>();
        Action = eEntityActions.idle;
    }

    public override void BrainUpdate()
    {
        if (Action == eEntityActions.idle)
        {
            IdleElapsed += Time.deltaTime;

            if (IdleElapsed >= IdleTime)
            {
                Action = eEntityActions.moving;

                Vector2 toTarget = Player.transform.position - Hub.transform.position;
                toTarget = toTarget.normalized;
                float angle = Random.Range(-MaxAngle, MaxAngle) * Mathf.Deg2Rad;
                TravelDir = toTarget.Rotate(angle).normalized;
                Hub.Mover.MoveVector = TravelDir;


                MoveElapsed = 0f;
                MoveTime = RandomDuration();
            }
        }
        else if (Action == eEntityActions.moving)
        {
            MoveElapsed += Time.deltaTime;

            if (MoveElapsed >= MoveTime)
            {
                Action = eEntityActions.idle;

                Hub.Mover.MoveVector = Vector2.zero;

                IdleElapsed = 0f;
                IdleTime = RandomDuration();
            }
        }
    }

    private float RandomDuration()
    {
        return Random.Range(0.5f, 1.5f);
    }
}
