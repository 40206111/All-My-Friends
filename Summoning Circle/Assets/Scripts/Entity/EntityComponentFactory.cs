using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityComponentFactory
{
    public static EntityBrain GetBrain(eEntityBrainComponent component, EntityHub hub)
    {
        return component switch
        {
            eEntityBrainComponent.entity => new EntityBrain(hub),
            eEntityBrainComponent.player => new PlayerBrain(hub),
            eEntityBrainComponent.enemy => new EnemyBrain(hub),
            eEntityBrainComponent.bounce => new BounceBrain(hub),
            eEntityBrainComponent.orbit => new OrbitBrain(hub),
            eEntityBrainComponent.sideDash => new SideDashBrain(hub),
            eEntityBrainComponent.follower => new FollowerBrain(hub),
            eEntityBrainComponent.scareyFol => new ScareyFolBrain(hub),
            eEntityBrainComponent.tentBoss => new TentacleBossBrain(hub),
            _ => null,
        };
    }
    public static HealthPool GetHealth(eEntityHealthComponent component, EntityHub hub)
    {
        return component switch
        {
            eEntityHealthComponent.player => new PlayerHealth(hub),
            eEntityHealthComponent.enemy => new EnemyHealth(hub),
            _ => null,
        };
    }
    public static EntityMover GetMover(eEntityMoveComponent component, EntityHub hub)
    {
        return component switch
        {
            eEntityMoveComponent.entity => new EntityMover(hub),
            _ => null,
        };
    }
    public static EntityProjCaster GetCaster(eEntityCastComponent component, EntityHub hub)
    {
        return component switch
        {
            eEntityCastComponent.entity => new EntityProjCaster(hub),
            eEntityCastComponent.spread => new SpreadCast(hub),
            _ => null,
        };
    }
}
