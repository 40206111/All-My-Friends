using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHub : MonoBehaviour
{
    public ushort FrozenMovement = 0;

    public EntityMover Mover;
    public EntityProjCaster Caster;
    public EntityBrain Brain;
    public HealthPool Health;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Mover = new EntityMover();
        Brain = new EntityBrain();

        Mover.Body = GetComponent<Rigidbody2D>();
        Mover.Hub = this;

        Brain.Hub = this;
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
        yield return null;
        Destroy(gameObject);
    }
}
