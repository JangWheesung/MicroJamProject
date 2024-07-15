using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HasitState
{
    Idle,
    Move,
    Attack,
    Stop,
    Die
}

public class HasitFSM : EnemyBase<HasitState>
{
    [Header("Hasit")]
    public AttackEffectBase circleEffect;
    [SerializeField] private Sprite[] breakSp;
    [SerializeField] private GaugeBarObj hpBar;
    [SerializeField] private int maxHp;
    private int hp;

    [Header("Hasit_Value")]
    public float jumpPower;
    public float moveSpeed;
    public float moveDelay;
    public float attackDelay;
    public float attackRange;

    protected override void InitializeState()
    {
        HasitIdleState idleState = new HasitIdleState(this);
        HasitMoveState moveState = new HasitMoveState(this);
        HasitAttackState attackState = new HasitAttackState(this);
        HasitStopState stopState = new HasitStopState(this);
        HasitDieState dieState = new HasitDieState(this);

        StateStorage<HasitState> liveState = new StateStorage<HasitState>(idleState, moveState, attackState);

        moveState.AddTransition(new HasitPlayerTransition(this, RangeType.In), HasitState.Attack);
        attackState.AddTransition(new HasitPlayerTransition(this, RangeType.Out), HasitState.Move);
        liveState.AddTransition(new HasitStopTransition(this), HasitState.Stop);
        liveState.AddTransition(new HasitDieTransition(this), HasitState.Die);
        stopState.AddTransition(new HasitDieTransition(this), HasitState.Die);

        AddState(idleState, HasitState.Idle);
        AddState(moveState, HasitState.Move);
        AddState(attackState, HasitState.Attack);
        AddState(stopState, HasitState.Stop);
        AddState(dieState, HasitState.Die);

        ChangeState(HasitState.Idle);
    }

    private void OnEnable()
    {
        hpBar.MaxValue = maxHp;
        hpBar.Value = maxHp;
        hp = maxHp;

        sp.sprite = breakSp[0];

        ChangeState(HasitState.Idle);
    }

    public override void Death(float minusTime)
    {
        hp--;
        hpBar.Value--;

        if (hp <= 0 || nowState == HasitState.Stop)
        {
            base.Death(minusTime);
        }
        else
        {
            BreakEffect();
        }
    }

    private void BreakEffect()
    {
        Slice slices = PoolingManager.Instance.Pop<Slice>(enemySlice.name, transform.position);
        slices.BreakEffect(6f);

        sp.sprite = breakSp[maxHp - hp];
    }
}
