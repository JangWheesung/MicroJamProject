using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CopeState
{
    Idle,
    Move,
    Count,
    Bomb,
    Stop,
    Die
};

public class CopeFSM : EnemyBase<CopeState>
{
    [Header("Cope")]
    public AttackEffectBase bombEffect;
    public EffectBase bombRangeEffect;
    public TMP_Text countText;

    public float moveSpeed;
    public float attackAmount;
    public float attackRange;
    public float countDelay;

    protected override void InitializeState()
    {
        CopeIdleState idleState = new CopeIdleState(this);
        CopeMoveState moveState = new CopeMoveState(this);
        CopeCountState countState = new CopeCountState(this);
        CopeBombState bombState = new CopeBombState(this);
        CopeStopState stopState = new CopeStopState(this);
        CopeDieState dieState = new CopeDieState(this);

        StateStorage<CopeState> liveState = new StateStorage<CopeState>(idleState, moveState, countState, bombState);

        moveState.AddTransition(new CopePlayerTransition(this), CopeState.Count);
        countState.AddTransition(new CopeTimeTransition(this, countDelay), CopeState.Bomb);
        liveState.AddTransition(new CopeStopTransition(this), CopeState.Stop);
        liveState.AddTransition(new CopeDieTransition(this), CopeState.Die);
        stopState.AddTransition(new CopeDieTransition(this), CopeState.Die);

        AddState(idleState, CopeState.Idle);
        AddState(moveState, CopeState.Move);
        AddState(countState, CopeState.Count);
        AddState(bombState, CopeState.Bomb);
        AddState(stopState, CopeState.Stop);
        AddState(dieState, CopeState.Die);

        ChangeState(CopeState.Idle);
    }

    public override void Upgrade()
    {
        base.Upgrade();

        moveSpeed += 1f;
    }

    private void OnEnable()
    {
        ChangeState(CopeState.Idle);
    }
}
