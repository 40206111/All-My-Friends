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
        Caster = new EntityProjCaster();
        Brain = new EntityBrain();
        Health = new EnemyHealth();

        Mover.Body = GetComponent<Rigidbody2D>();
        Mover.Hub = this;

        Caster.Hub = this;

        Brain.Hub = this;

        Health.Hub = this;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Brain.BrainUpdate();
        Mover.MoveUpdate();
        Caster.CastUpdate();
    }
}
