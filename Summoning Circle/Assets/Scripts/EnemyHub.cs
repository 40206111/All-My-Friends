using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHub : EntityHub
{
    protected override void Start()
    {
        Mover = new EntityMover(this);
        Brain = new EnemyBrain(this);
        Health = new EnemyHealth(this);

        Mover.Body = GetComponent<Rigidbody2D>();
        Mover.Speed /= 3f;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHub phub = collision.collider.GetComponent<PlayerHub>();
                phub.Health.Damage(1);
            phub.Mover.Knockback(((Vector2)(phub.transform.position - transform.position)).normalized * 4.0f);
        }
    }
}
