using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBrain : EntityBrain
{
    protected Vector2 TravelDir = (2f * Vector2.right + Vector2.down).normalized;
    protected float CollisionTime = -1.0f;
    protected float TickTime = 0.0f;
    protected float TickActivationTime = 0.2f;

    public int Damage = 10;

    protected List<HealthPool> Overlaps = new List<HealthPool>();

    public BounceBrain(EntityHub hub) : base(hub)
    {
        Hub.OnCollisionEnter += OnCollisionEnter2D;
        Hub.OnCollisionExit += OnCollisionExit2D;
        Hub.OnTriggerEnter += OnTriggerEnter2D;
        Hub.OnTriggerExit += OnTriggerExit2D;
    }

    ~BounceBrain()
    {
        Hub.OnCollisionEnter -= OnCollisionEnter2D;
        Hub.OnCollisionExit -= OnCollisionExit2D;
        Hub.OnTriggerEnter -= OnTriggerEnter2D;
        Hub.OnTriggerExit -= OnTriggerExit2D;
    }

    public override void BrainUpdate()
    {
        Hub.Mover.MoveVector = TravelDir;

        if (CollisionTime >= 0f)
        {
            CollisionTime += Time.deltaTime;
            if (CollisionTime > 2.0f)
            {
                Debug.LogError("Bouncer was stuck");
                PlayerHub player = Object.FindObjectOfType<PlayerHub>();
                if (player != null)
                {
                    Hub.transform.position = player.transform.position;
                    CollisionTime = -1f;
                }
            }
        }

        TickTime += Time.deltaTime;
        if (TickTime > TickActivationTime)
        {
            TickTime -= TickActivationTime;
            foreach (HealthPool pool in Overlaps)
            {
                pool.Damage(Damage);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionTime = 0f;

        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        collision.GetContacts(contacts);

        List<Vector2> usedNorms = new List<Vector2>();

        foreach (ContactPoint2D cp in contacts)
        {
            //Vector2 dirToPoint = (cp.point - (Vector2)Hub.transform.position).normalized;
            //bool doContinue = false;
            //foreach (Vector2 v in usedPoints)
            //{
            //    if (Vector2.Dot(v, dirToPoint) > 0.9)
            //    {
            //        doContinue = true;
            //        break;
            //    }
            //}
            //if (doContinue) { continue; }

            //float signAngle = Vector2.SignedAngle(TravelDir, dirToPoint);

            //TravelDir = (Quaternion.Euler(0, 0, 180f + signAngle * 2f) * (Vector3)TravelDir).normalized;

            //Hub.Mover.Body.velocity = TravelDir * Hub.Mover.Speed * 0.5f;
            //Hub.transform.Translate(TravelDir * Hub.Mover.Speed * 0.5f * Time.deltaTime);

            Vector2 norm = cp.normal;
            bool doContinue = false;
            foreach (Vector2 v in usedNorms)
            {
                if (Vector2.Dot(v, norm) > 0.9)
                {
                    doContinue = true;
                    break;
                }
            }
            if (doContinue) { continue; }
            if (Mathf.Abs(norm.x) > 0.5)
            {
                TravelDir.x = -TravelDir.x;
            }
            if (Mathf.Abs(norm.y) > 0.5)
            {
                TravelDir.y = -TravelDir.y;
            }

            Hub.Mover.Body.velocity = 0.5f * Hub.Mover.Speed * TravelDir;
            Hub.transform.Translate(0.5f * Hub.Mover.Speed * Time.deltaTime * TravelDir);

            usedNorms.Add(norm);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        CollisionTime = -1f;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        EntityHub hub = collision.GetComponent<EntityHub>();
        if (hub != null && hub.Health != null)
        {
            if (!Overlaps.Contains(hub.Health))
            {
                Overlaps.Add(hub.Health);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        EntityHub hub = collision.GetComponent<EntityHub>();
        if (hub != null && hub.Health != null)
        {
            if (Overlaps.Contains(hub.Health))
            {
                Overlaps.Remove(hub.Health);
            }
        }
    }
}
