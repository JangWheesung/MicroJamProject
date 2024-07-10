using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StigmaBaseState : BaseState<StigmaState>
{
    protected StigmaFSM stigmaFSM;

    protected Animator anim;
    protected AttackEffectBase smashinEffect;

    public float jumpPower;
    public float moveSpeed;
    public float attackRange;

    public StigmaBaseState(StigmaFSM fsm) : base(fsm)
    {
        stigmaFSM = fsm;

        anim = fsm.anim;
        smashinEffect = fsm.smashinEffect;

        jumpPower = fsm.jumpPower;
        moveSpeed = fsm.moveSpeed;
        attackRange = fsm.attackRange;
    }
}
