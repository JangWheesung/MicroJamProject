using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HasitBaseState : BaseState<HasitState>
{
    protected HasitFSM hasitFSM;

    protected AttackEffectBase circleEffect;

    protected float jumpPower;
    protected float moveSpeed;
    protected float moveDelay;
    protected float attackDelay;
    protected float attackRange;

    public HasitBaseState(HasitFSM fsm) : base(fsm)
    {
        hasitFSM = fsm;

        circleEffect = fsm.circleEffect;

        jumpPower = fsm.jumpPower;
        moveSpeed = fsm.moveSpeed;
        moveDelay = fsm.moveDelay;
        attackDelay = fsm.attackDelay;
        attackRange = fsm.attackRange;
    }
}
