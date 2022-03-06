using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHub : EntityHub
{
    protected override void Start()
    {
        Mover = new EntityMover();
        Caster = new EntityProjCaster();
        Brain = new EnemyBrain();
        Health = new EnemyHealth();

        Mover.Body = GetComponent<Rigidbody2D>();
        Mover.Hub = this;
        Mover.Speed /= 3f;

        Caster.Hub = this;

        Brain.Hub = this;

        Health.Hub = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerHub>().Health.Damage(1);
        }
    }
}
