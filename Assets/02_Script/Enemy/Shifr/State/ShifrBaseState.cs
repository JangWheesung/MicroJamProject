using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrBaseState : BaseState<ShifrState>
{
    protected ShifrFSM shifrFSM;

    protected AttackEffectBase circleEffect;

    protected float jumpPower;
    protected float moveSpeed;
    protected float attackDelay;

    public ShifrBaseState(ShifrFSM fsm) : base(fsm)
    {
        shifrFSM = fsm;

        circleEffect = fsm.circleEffect;

        jumpPower = fsm.jumpPower;
        moveSpeed = fsm.moveSpeed;
        attackDelay = fsm.attackDelay;
    }
}
