using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityComponentFactory
{
    public static EntityBrain GetBrain(eEntityComponent component, EntityHub hub)
    {
        switch (component)
        {
            case eEntityComponent.entity:
                return new EntityBrain(hub);
            case eEntityComponent.player:
                return new PlayerBrain(hub);
            case eEntityComponent.enemy:
                return new EnemyBrain(hub);
            case eEntityComponent.bounce:
                return new BounceBrain(hub);
            case eEntityComponent.orbit:
                return new OrbitBrain(hub);
            case eEntityComponent.sideDash:
                return new SideDashBrain(hub);
            default:
                return null;
        }
    }
}
