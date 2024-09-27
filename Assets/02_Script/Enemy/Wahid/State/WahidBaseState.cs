using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WahidBaseState : BaseState<WahidState>
{
    protected WahidFSM wahidFSM;

    protected AttackEffectBase meleeAttackEffect;
    protected AttackEffectBase rangeAttackEffect;

    protected float waveAmount;
    protected float waveInterval;
    protected float moveSpeed;
    protected float attackAmount;
    protected float meleeDelay;
    protected float rangeDelay;
    protected float meleeRange;
    protected float tweenRange;
    protected float rangeRange;

    public WahidBaseState(WahidFSM fsm) : base(fsm)
    {
        wahidFSM = fsm;

        meleeAttackEffect = fsm.meleeAttackEffect;
        rangeAttackEffect = fsm.rangeAttackEffect;

        waveAmount = fsm.waveAmount;
        waveInterval = fsm.waveInterval;
        moveSpeed = fsm.moveSpeed;
        attackAmount = fsm.attackAmount;
        meleeDelay = fsm.meleeDelay;
        rangeDelay = fsm.rangeDelay;
        meleeRange = fsm.meleeRange;
        tweenRange = fsm.tweenRange;
        rangeRange = fsm.rangeRange;
    }
}
