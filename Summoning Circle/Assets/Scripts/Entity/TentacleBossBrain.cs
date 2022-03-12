using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTimer
{
    public float Elapsed = 0f;
    public float Time = 1f;

    public bool IsFinished => Elapsed >= Time;
    public void Update(float dt) { Elapsed += dt; }
    public void NewTime(float time) { Time = time; Elapsed = 0f; }
}

public struct ActionStateTimes
{
    private readonly float idle;
    private readonly float windUp;
    private readonly float inProgress;
    private readonly float windDown;

    public ActionStateTimes(float idle, float up, float inP, float down)
    {
        this.idle = idle;
        windUp = up;
        inProgress = inP;
        windDown = down;
    }

    public float GetTime(eActionState state)
    {
        return state switch
        {
            eActionState.idle => idle,
            eActionState.windUp => windUp,
            eActionState.inProgress => inProgress,
            eActionState.windDown => windDown,
            _ => idle
        };
    }
}

public enum eActionState { none = -1, idle, windUp, inProgress, windDown }
public class TentacleBossBrain : EntityBrain
{
    enum eTBossAction { none = -1, idle, smash, cast }
    eTBossAction Action = eTBossAction.none;
    eActionState ActionState = eActionState.none;

    readonly ActionStateTimes SmashTimes = new ActionStateTimes(1f, 0.5f, 0.5f, 0.2f);
    readonly ActionStateTimes CastTimes = new ActionStateTimes(1f, 0.5f, 0.2f, 0.2f);

    StateTimer Idle = new StateTimer { Time = 2f };
    StateTimer Smash = new StateTimer { Time = 10f };
    StateTimer Cast = new StateTimer { Time = 5f };
    StateTimer State = new StateTimer { Time = 0.5f };

    Vector2 TargetPos = Vector2.zero;
    TentacleSmash Tentacle;

    public TentacleBossBrain(EntityHub hub) : base(hub)
    {
        Action = eTBossAction.idle;
        Tentacle = hub.GetComponentInChildren<TentacleSmash>();
    }

    public override void BrainUpdate()
    {
        switch (Action)
        {
            case eTBossAction.idle:
                Idle.Update(Time.deltaTime);
                if (Idle.Elapsed >= Idle.Time)
                {
                    Action = RandomAction();
                    ActionState = eActionState.idle;
                    State.Time = CurrTimes().GetTime(ActionState);
                    Idle.Elapsed = 0f;
                }
                break;
            case eTBossAction.smash:
                Smash.Update(Time.deltaTime);
                if (Smash.IsFinished && ActionState == eActionState.idle)
                {
                    
                    Action = eTBossAction.idle;
                    Smash.Elapsed = 0f;
                }
                switch (ActionState)
                {
                    case eActionState.idle:
                        State.Update(Time.deltaTime);
                        if (State.IsFinished)
                        {
                            ActionState = NextState(ActionState);
                            State.NewTime(CurrTimes().GetTime(ActionState));

                            WindUpStart();
                        }
                        break;
                    case eActionState.windUp:
                        State.Update(Time.deltaTime);
                        if (State.IsFinished)
                        {
                            ActionState = NextState(ActionState);
                            State.NewTime(CurrTimes().GetTime(ActionState));

                            InProgressStart();
                        }
                        break;
                    case eActionState.inProgress:
                        State.Update(Time.deltaTime);
                        if (State.IsFinished)
                        {
                            ActionState = NextState(ActionState);
                            State.NewTime(CurrTimes().GetTime(ActionState));

                            Tentacle.State = ActionState;
                        }
                        break;
                    case eActionState.windDown:
                        State.Update(Time.deltaTime);
                        if (State.IsFinished)
                        {
                            ActionState = NextState(ActionState);
                            State.NewTime(CurrTimes().GetTime(ActionState));

                            Tentacle.State = ActionState;
                        }
                        break;
                }
                break;
            case eTBossAction.cast:
                Action = eTBossAction.smash;
                break;
        }
    }


    private void WindUpStart()
    {
        switch (Action)
        {
            case eTBossAction.smash:
                TargetPos = PlayerHub.Instance.transform.position;
                Tentacle.NewTarget(TargetPos);
                break;
            case eTBossAction.cast:
                break;
            default:
                break;
        }
    }

    private void InProgressStart()
    {
        switch (Action)
        {
            case eTBossAction.smash:
                Tentacle.DoAttack();
                break;
            case eTBossAction.cast:
                break;
            default:
                break;
        }
    }

    private ActionStateTimes CurrTimes()
    {
        return Action switch
        {
            eTBossAction.smash => SmashTimes,
            eTBossAction.cast => CastTimes,
            _ => default
        };
    }

    private eTBossAction RandomAction()
    {
        return Random.Range(0, 2) switch
        {
            0 => eTBossAction.smash,
            1 => eTBossAction.cast,
            _ => eTBossAction.idle
        };
    }

    private eActionState NextState(eActionState state)
    {
        return state switch
        {
            eActionState.idle => eActionState.windUp,
            eActionState.windUp => eActionState.inProgress,
            eActionState.inProgress => eActionState.windDown,
            eActionState.windDown => eActionState.idle
        };
    }
}
