using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMover
{
    // Start is called before the first frame update
    public enum eMovementType { none = -1, direction, goal }

    public Rigidbody2D Body;
    public EntityHub Hub;

    public float Speed = 5.0f;
    public float Accel = 25.0f;

    public Vector2 MoveVector = Vector2.zero;

    public EntityMover(EntityHub hub)
    {
        Hub = hub;
        Body = Hub.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void MoveUpdate()
    {
        if(Hub.FrozenMovement == 0)
        {
            Vector2 velChange = MoveVector * Speed - Body.velocity;
            Vector2 accel = velChange.normalized * Accel;
            Vector2 addVal = accel * Time.deltaTime;
            if(addVal.sqrMagnitude > velChange.sqrMagnitude)
            {
                addVal = velChange;
            }
            Body.velocity += addVal;
        }
    }

    public void Knockback(Vector2 knockback)
    {
        Body.velocity += knockback;
    }
}
