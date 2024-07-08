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
    public AttackEffeectBase circleEffect;

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

        ShifrInPlayerTransition inPlayerTransition = new ShifrInPlayerTransition(this);
        ShifrOutPlayerTransition outPlayerTransition = new ShifrOutPlayerTransition(this);
        ShifrStopTransition stopTransition = new ShifrStopTransition(this);
        ShifrDieTransition dieTransition = new ShifrDieTransition(this);

        StateStorage<ShifrState> liveState = new StateStorage<ShifrState>(idleState, moveState, attackState);

        moveState.AddTransition(inPlayerTransition, ShifrState.Attack);
        attackState.AddTransition(outPlayerTransition, ShifrState.Move);
        liveState.AddTransition(stopTransition, ShifrState.Stop);
        liveState.AddTransition(dieTransition, ShifrState.Die);
        stopState.AddTransition(dieTransition, ShifrState.Die);

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
