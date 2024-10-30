using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move로 플레이어한테 이동(MoveState)
//특정 범위 안에 들면 사라짐(FadeState)
//혹은 죽을때 낮은 확률로 사라짐(FadeState)
//FadeState에서 n초가 전부 지나기 0.5~1초 전 플레이어의 양 옆에서 나타나는듯한 파티클 빵(위치 고정)
//공격은 창이나 표창같은 긴 범위
//공격이 성공하든 실패하든 IdleState로 전환
//IdleState에서 1초뒤 다시 FadeState

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