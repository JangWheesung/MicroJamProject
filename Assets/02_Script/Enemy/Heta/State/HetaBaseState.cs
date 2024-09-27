using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaBaseState : BaseState<HetaState>
{
    protected HetaFSM hetaFSM;

    protected AttackEffectBase lazerEffect;
    protected EffectBase crosshairEffect;

    protected float jumpPower;
    protected float moveSpeed;
    protected float attackAmount;
    protected float aimingDelay;
    protected float attackDelay;
    protected float attackRange;

    public HetaBaseState(HetaFSM fsm) : base(fsm)
    {
        hetaFSM = fsm;

        lazerEffect = fsm.lazerEffect;
        crosshairEffect = fsm.crosshairEffect;

        jumpPower = fsm.jumpPower;
        moveSpeed = fsm.moveSpeed;
        attackAmount = fsm.attackAmount;
        aimingDelay = fsm.aimingDelay;
        attackDelay = fsm.attackDelay;
        attackRange = fsm.attackRange;
    }
}
