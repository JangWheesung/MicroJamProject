using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangeType
{
    Out,
    In,
    Tween
}

public class RangeTransition<T> : BaseTransition<T> where T : Enum
{
    private RangeType rangeType;
    private float rangeValue;
    private float minValue, maxValue;

    public RangeTransition(EnemyBase<T> fsm, float range, RangeType type = RangeType.Out) : base(fsm)
    {
        rangeValue = range;
        rangeType = type;
    }

    public RangeTransition(EnemyBase<T> fsm, float min, float max) : base(fsm)
    {
        minValue = min; maxValue = max;
        rangeType = RangeType.Tween;
    }

    public override bool CheckTransition()
    {
        float distance = Vector2.Distance(enemyBase.transform.position, playerTrs.position);
        bool InRange = rangeType switch
        {
            RangeType.In => (distance <= rangeValue),
            RangeType.Out => (distance > rangeValue),
            RangeType.Tween => (minValue < distance) && (distance <= maxValue),
            _ => false
        };
        return InRange;
    }
}
