using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityProjCaster
{
    public Vector2 CastDirection = Vector2.zero;

    public EntityHub Hub;

    public float CastCDTime = 0.5f;
    public float CastCDRemaining = 0.0f;

    public EntityProjCaster(EntityHub hub)
    {
        Hub = hub;
    }

    public virtual void CastUpdate()
    {
        if (CastCDRemaining <= 0f)
        {
            if (CastDirection != Vector2.zero)
            {
                if (CastDirection.sqrMagnitude != 1f)
                {
                    CastDirection = CastDirection.normalized;
                }

                float angle = Vector2.Angle(CastDirection, Vector2.up);
                float sign = Mathf.Sign(Vector2.Dot(CastDirection, Vector2.left));

                angle /= 45f;
                angle = Mathf.Round(angle);
                angle *= 45f;

                Vector2 castDir = Quaternion.Euler(0, 0, angle * sign) * Vector2.up;

                Projectile p = Object.Instantiate((Hub as PlayerHub).Projectile, Hub.transform.position + (Vector3)castDir, Quaternion.identity).GetComponent<Projectile>();
                p.Direction = castDir.normalized;

                CastCDRemaining = CastCDTime;
            }
        }
        else
        {
            CastCDRemaining -= Time.deltaTime;
        }

    }

}
