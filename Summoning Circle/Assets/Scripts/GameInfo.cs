using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum eFaction { none = 0, neutral = 1 << 0, player = 1 << 1, enemy = 1 << 2, all = neutral | player | enemy }

public enum eEntityState { none = -1, spawning, living, dieing }

[System.Flags]
public enum eEntityActions { none = 0, idle = 1 << 0, moving = 1 << 1, attacking = 1 << 2 }

public enum eEntityBrainComponent { none = -1, entity, player, enemy, bounce, orbit, sideDash, follower, scareyFol, tentBoss }
public enum eEntityMoveComponent { none = -1, entity }
public enum eEntityCastComponent { none = -1, entity, spread }
public enum eEntityHealthComponent { none = -1, player, enemy}

public static class GameInfo
{

}
