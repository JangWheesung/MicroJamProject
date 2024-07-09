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

        HetaInPlayerTransition playerTransition = new HetaInPlayerTransition(this);
        HetaTimeTransition attackTimeTransition = new HetaTimeTransition(this, aimingDelay);
        HetaTimeTransition delayTimeTransition = new HetaTimeTransition(this, attackDelay);
        HetaStopTransition stopTransition = new HetaStopTransition(this);
        HetaDieTransition dieTransition = new HetaDieTransition(this);

        StateStorage<HetaState> liveState = new StateStorage<HetaState>(idleState, moveState, aimingState, attackState);

        moveState.AddTransition(playerTransition, HetaState.Aiming);
        aimingState.AddTransition(attackTimeTransition, HetaState.Attack);
        attackState.AddTransition(delayTimeTransition, HetaState.Idle);
        liveState.AddTransition(stopTransition, HetaState.Stop);
        liveState.AddTransition(dieTransition, HetaState.Die);
        stopState.AddTransition(dieTransition, HetaState.Die);

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
