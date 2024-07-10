using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShifrState
{
    Idle,
    Move,
    Attack,
    Stop,
    Die
};

public class ShifrFSM : EnemyBase<ShifrState>
{
    [Header("Shifr")]
    public AttackEffectBase circleEffect;

    public float jumpPower;
    public float moveSpeed;
    public float attackDelay;
    public float attackRange;

    protected override void InitializeState()
    {
        ShifrIdleState idleState = new ShifrIdleState(this);
        ShifrMoveState moveState = new ShifrMoveState(this);
        ShifrAttackState attackState = new ShifrAttackState(this);
        ShifrStopState stopState = new ShifrStopState(this);
        ShifrDieState dieState = new ShifrDieState(this);

        StateStorage<ShifrState> liveState = new StateStorage<ShifrState>(idleState, moveState, attackState);

        moveState.AddTransition(new ShifrPlayerTransition(this, RangeType.In), ShifrState.Attack);
        attackState.AddTransition(new ShifrPlayerTransition(this, RangeType.Out), ShifrState.Move);
        liveState.AddTransition(new ShifrStopTransition(this), ShifrState.Stop);
        liveState.AddTransition(new ShifrDieTransition(this), ShifrState.Die);
        stopState.AddTransition(new ShifrDieTransition(this), ShifrState.Die);

        AddState(idleState, ShifrState.Idle);
        AddState(moveState, ShifrState.Move);
        AddState(attackState, ShifrState.Attack);
        AddState(stopState, ShifrState.Stop);
        AddState(dieState, ShifrState.Die);

        ChangeState(ShifrState.Idle);
    }

    private void OnEnable()
    {
        ChangeState(ShifrState.Idle);
    }
}
