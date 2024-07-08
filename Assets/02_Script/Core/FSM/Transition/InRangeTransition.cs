using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeTransition<T> : BaseTransition<T> where T : Enum
{
    private float rangeValue;

    public InRangeTransition(EnemyBase<T> fsm, float range) : base(fsm)
    {
        rangeValue = range;
    }

    public override bool CheckTransition()
    {
        bool InRange = Vector2.Distance(enemyBase.transform.position, playerTrs.position) <= rangeValue;
        return InRange;
    }
}
