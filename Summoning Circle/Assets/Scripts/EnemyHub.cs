using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHub : EntityHub
{
    protected override void Start()
    {
        Mover = new EntityMover();
        Brain = new EnemyBrain();
        Health = new EnemyHealth();

        Mover.Body = GetComponent<Rigidbody2D>();
        Mover.Hub = this;
        Mover.Speed /= 3f;

        Brain.Hub = this;

        Health.Hub = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHub phub = collision.collider.GetComponent<PlayerHub>();
                phub.Health.Damage(1);
            phub.Mover.Knockback(((Vector2)(phub.transform.position - transform.position)).normalized * 4.0f);
        }
    }
}
