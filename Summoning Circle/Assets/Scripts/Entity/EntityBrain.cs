using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBrain
{
    public EntityHub Hub;

    public EntityBrain(EntityHub hub)
    {
        Hub = hub;
    }
    public virtual void Destroy() { }

    public virtual void BrainUpdate()
    {
    }
}
