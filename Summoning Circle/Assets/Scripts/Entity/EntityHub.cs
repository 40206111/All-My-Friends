using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityHub : MonoBehaviour
{
    public ushort FrozenMovement = 0;

    public eFaction Faction;

    public Transform Projectile;

    public EntityMover Mover;
    public EntityProjCaster Caster;
    public EntityBrain Brain;
    public HealthPool Health;
    public ProjectileBlocker PBlocker;

    public Action<Collision2D> OnCollisionEnter;
    public Action<Collision2D> OnCollisionExit;
    public Action<Collider2D> OnTriggerEnter;
    public Action<Collider2D> OnTriggerExit;

    public Action<EntityHub> OnDeath;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Faction = eFaction.neutral;

        Mover = new EntityMover(this);
        Brain = new EntityBrain(this);
        Caster = new EntityProjCaster(this);

        Caster.Data.TargetFaction = eFaction.player;
        Caster.Data.SourceFaction = eFaction.enemy;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Brain?.BrainUpdate();
        Mover?.MoveUpdate();
        Caster?.CastUpdate();
        Health?.HealthUpdate();
    }

    public virtual void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    protected virtual IEnumerator DieCoroutine()
    {
        OnDeath?.Invoke(this);
        yield return null;
        Destroy(gameObject);
    }



    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter?.Invoke(collision);
    }
    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExit?.Invoke(collision);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter?.Invoke(collision);
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExit?.Invoke(collision);
    }
}
