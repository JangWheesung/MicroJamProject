using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CopeBaseState : BaseState<CopeState>
{
    protected CopeFSM copeFSM;

    protected AttackEffectBase bombEffect;
    protected EffectBase bombRangeEffect;
    protected TMP_Text countText;

    protected float moveSpeed;
    protected float countDelay;
    protected float attackRange;

    public CopeBaseState(CopeFSM fsm) : base(fsm)
    {
        copeFSM = fsm;

        bombEffect = fsm.bombEffect;
        bombRangeEffect = fsm.bombRangeEffect;
        countText = fsm.countText;

        moveSpeed = fsm.moveSpeed;
        countDelay = fsm.countDelay;
        attackRange = fsm.attackRange;
    }
}
