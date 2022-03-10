using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData Data;

    public Vector2 Origin;
    public Vector2 Direction;

    public bool IsInitialised = false;

    public void Initialise(Vector2 origin, Vector2 dir, ProjectileData data)
    {

        Origin = origin;
        Direction = dir;

        Data = data;

        transform.position = origin;
        IsInitialised = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInitialised)
        {
            return;
        }
        transform.position += (Vector3)Direction * Data.Speed * Time.deltaTime;
        if (((Vector2)transform.position - Origin).sqrMagnitude > Data.Range * Data.Range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool destroy = false;
        if (collision.CompareTag("Solid"))
        {
            destroy = true;
        }
        else
        {
            EntityHub hub = collision.GetComponent<EntityHub>();
            if (hub.Health != null && (hub.Faction & Data.TargetFaction) != eFaction.none)
            { 
                hub.Health.Damage(Data.Damage);
                destroy = true;
            }
        }

        if (destroy)
        {
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        yield return null;
        Destroy(gameObject);
    }
}
