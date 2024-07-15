using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WahidState
{
    Idle,
    Move,
    MeleeAttack,
    RangeAttack,
    Stop,
    Die
}

public class WahidFSM : EnemyBase<WahidState>
{
    public AttackEffectBase meleeAttackEffect;
    public AttackEffectBase rangeAttackEffect;

    public float waveAmount;
    public float waveInterval;
    public float moveSpeed;
    public float meleeDelay;
    public float rangeDelay;
    public float meleeRange;
    public float tweenRange;
    public float rangeRange;

    protected override void InitializeState()
    {
        WahidIdleState idleState = new WahidIdleState(this);
        WahidMoveState moveState = new WahidMoveState(this);
        WahidMeleeAttackState meleeAttackState = new WahidMeleeAttackState(this);
        WahidRangeAttackState rangeAttackState = new WahidRangeAttackState(this);
        WahidStopState stopState = new WahidStopState(this);
        WahidDieState dieState = new WahidDieState(this);

        StateStorage<WahidState> liveState = new StateStorage<WahidState>(idleState, moveState, meleeAttackState, rangeAttackState);

        moveState.AddTransition(new WahidPlayerTransition(this, tweenRange, rangeRange), WahidState.RangeAttack);
        moveState.AddTransition(new WahidPlayerTransition(this,  meleeRange, RangeType.In), WahidState.MeleeAttack);

        rangeAttackState.AddTransition(new WahidPlayerTransition(this, tweenRange, RangeType.In), WahidState.Move);
        rangeAttackState.AddTransition(new WahidPlayerTransition(this, rangeRange, RangeType.Out), WahidState.Move);
        meleeAttackState.AddTransition(new WahidPlayerTransition(this, meleeRange, RangeType.Out), WahidState.Move);
        
        liveState.AddTransition(new WahidStopTransition(this), WahidState.Stop);
        liveState.AddTransition(new WahidDieTransition(this), WahidState.Die);
        stopState.AddTransition(new WahidDieTransition(this), WahidState.Die);

        AddState(idleState, WahidState.Idle);
        AddState(moveState, WahidState.Move);
        AddState(meleeAttackState, WahidState.MeleeAttack);
        AddState(rangeAttackState, WahidState.RangeAttack);
        AddState(stopState, WahidState.Stop);
        AddState(dieState, WahidState.Die);

        ChangeState(WahidState.Idle);
    }

    private void OnEnable()
    {
        ChangeState(WahidState.Idle);
    }
}
