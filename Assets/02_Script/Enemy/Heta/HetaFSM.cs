using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HetaState
{
    Idle,
    Move,
    Aiming,
    Attack,
    Stop,
    Die
};

public class HetaFSM : EnemyBase<HetaState>
{
    [Header("Heta")]
    public AttackEffectBase lazerEffect;
    public EffectBase crosshairEffect;

    public LineRenderer aimingLine;

    public float jumpPower;
    public float moveSpeed;
    public float attackAmount;
    public float aimingDelay;
    public float attackDelay;
    public float attackRange;

    protected override void InitializeState()
    {
        HetaIdleState idleState = new HetaIdleState(this);
        HetaMoveState moveState = new HetaMoveState(this);
        HetaAimingState aimingState = new HetaAimingState(this);
        HetaAttackState attackState = new HetaAttackState(this);
        HetaStopState stopState = new HetaStopState(this);
        HetaDieState dieState = new HetaDieState(this);

        StateStorage<HetaState> liveState = new StateStorage<HetaState>(idleState, moveState, aimingState, attackState);

        moveState.AddTransition(new HetaInPlayerTransition(this, RangeType.In), HetaState.Aiming);
        aimingState.AddTransition(new HetaTimeTransition(this, aimingDelay), HetaState.Attack);
        attackState.AddTransition(new HetaTimeTransition(this, attackDelay), HetaState.Idle);
        liveState.AddTransition(new HetaStopTransition(this), HetaState.Stop);
        liveState.AddTransition(new HetaDieTransition(this), HetaState.Die);
        stopState.AddTransition(new HetaDieTransition(this), HetaState.Die);

        AddState(idleState, HetaState.Idle);
        AddState(moveState, HetaState.Move);
        AddState(aimingState, HetaState.Aiming);
        AddState(attackState, HetaState.Attack);
        AddState(stopState, HetaState.Stop);
        AddState(dieState, HetaState.Die);

        ChangeState(HetaState.Idle);
    }

    private void OnEnable()
    {
        ChangeState(HetaState.Idle);
    }
}
