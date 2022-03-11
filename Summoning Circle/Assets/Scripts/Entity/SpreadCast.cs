using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadCast : EntityProjCaster
{
    public PlayerHub Player;

    public float Count = 5;
    public float Width = MathUtils.Tau / 6f;

    public SpreadCast(EntityHub hub) : base(hub)
    {
        Player = Object.FindObjectOfType<PlayerHub>();
    }

    // Update is called once per frame
    public override void CastUpdate()
    {
        if (Input.GetMouseButtonDown(4))
        {
            Vector2 start = ((Vector2)(Player.transform.position - Hub.transform.position)).normalized.Rotate(Width / 2f).normalized;
            for (int i = 0; i < Count; ++i)
            {
                SpawnProjectile(start);
                start = start.Rotate(-Width / (Count-1f));
            }
        }
    }
}
