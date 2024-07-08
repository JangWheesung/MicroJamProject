using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrBaseState : BaseState<ShifrState>
{
    protected ShifrFSM wowFSM;

    protected AttackEffeectBase circleEffect;

    protected float jumpPower;
    protected float moveSpeed;
    protected float attackDelay;

    public ShifrBaseState(ShifrFSM fsm) : base(fsm)
    {
        wowFSM = fsm;

        this.circleEffect = fsm.circleEffect;

        jumpPower = fsm.jumpPower;
        moveSpeed = fsm.moveSpeed;
        attackDelay = fsm.attackDelay;
    }
}
