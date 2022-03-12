using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityProjCaster
{
    public Vector2 CastDirection = Vector2.zero;

    public EntityHub Hub;
        Collider2D HubCollider;

    public float CastCDRemaining = 0.0f;

    public ProjectileData Data;

    public EntityProjCaster(EntityHub hub)
    {
        Hub = hub;
        HubCollider = hub.GetComponent<Collider2D>();
        Data = new ProjectileData(1, 1, 1, 1, eFaction.neutral, eFaction.neutral);
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
                CalculateProjectile();
                CastCDRemaining = 1.0f / Data.ShotsPerSecond;
            }
        }
        else
        {
            CastCDRemaining -= Time.deltaTime;
        }

    }

    protected virtual void CalculateProjectile()
    {
        float angle = Vector2.Angle(CastDirection, Vector2.up);
        float sign = Mathf.Sign(Vector2.Dot(CastDirection, Vector2.left));

        angle /= 45f;
        angle = Mathf.Round(angle);
        angle *= 45f;

        Vector2 castDir = Quaternion.Euler(0, 0, angle * sign) * Vector2.up;

        SpawnProjectile(castDir);
    }

    protected void SpawnProjectile(Vector2 dir)
    {
        Projectile p = Object.Instantiate(Hub.Projectile).GetComponent<Projectile>();

        p.Initialise(Hub.transform.position + (Vector3)dir, dir.normalized, Data);

        if (HubCollider != null)
        {
            Physics2D.IgnoreCollision(p.GetComponent<Collider2D>(), HubCollider);
        }
    }
}
