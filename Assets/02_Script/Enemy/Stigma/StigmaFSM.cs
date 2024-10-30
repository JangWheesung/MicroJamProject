using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move�� �÷��̾����� �̵�(MoveState)
//Ư�� ���� �ȿ� ��� �����(FadeState)
//Ȥ�� ������ ���� Ȯ���� �����(FadeState)
//FadeState���� n�ʰ� ���� ������ 0.5~1�� �� �÷��̾��� �� ������ ��Ÿ���µ��� ��ƼŬ ��(��ġ ����)
//������ â�̳� ǥâ���� �� ����
//������ �����ϵ� �����ϵ� IdleState�� ��ȯ
//IdleState���� 1�ʵ� �ٽ� FadeState

public enum StigmaState
{
    Idle,
    Move,
    Fade,
    Attack,
    Stop,
    Die
};

public class StigmaFSM : EnemyBase<StigmaState>
{
    [Header("Stigma")]
    public Animator anim;
    public AttackEffectBase smashinEffect;

    public float jumpPower;
    public float moveSpeed;
    public float fadeDelay;
    public float attackAmount;
    public float attackDelay;
    public float attackRange;
    [HideInInspector] public float gravityScale;

    protected override void Awake()
    {
        base.Awake();

        gravityScale = rb.gravityScale;
    }

    protected override void InitializeState()
    {
        StigmaIdleState idleState = new StigmaIdleState(this);
        StigmaMoveState moveState = new StigmaMoveState(this);
        StigmaFadeState fadeState = new StigmaFadeState(this);
        StigmaAttackState attackState = new StigmaAttackState(this);
        StigmaStopState stopState = new StigmaStopState(this);
        StigmaDieState dieState = new StigmaDieState(this);

        StateStorage<StigmaState> liveState = new StateStorage<StigmaState>(idleState, moveState, fadeState, attackState);

        moveState.AddTransition(new StigmaInPlayerTransition(this, RangeType.In), StigmaState.Fade);
        fadeState.AddTransition(new StigmaTimeTransition(this, fadeDelay), StigmaState.Attack);
        attackState.AddTransition(new StigmaTimeTransition(this, attackDelay), StigmaState.Idle);
        liveState.AddTransition(new StigmaStopTransition(this), StigmaState.Stop);
        liveState.AddTransition(new StigmaDieTransition(this), StigmaState.Die);
        stopState.AddTransition(new StigmaDieTransition(this), StigmaState.Die);

        AddState(idleState, StigmaState.Idle);
        AddState(moveState, StigmaState.Move);
        AddState(fadeState, StigmaState.Fade);
        AddState(attackState, StigmaState.Attack);
        AddState(stopState, StigmaState.Stop);
        AddState(dieState, StigmaState.Die);

        ChangeState(StigmaState.Idle);
    }

    private void OnEnable()
    {
        ChangeState(StigmaState.Idle);
    }

    public void SetSpriteColorA(float value)
    {
        Color c = sp.color;
        c.a = value;
        sp.color = c;
    }
}