using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WahidPlayerTransition : RangeTransition<WahidState>
{
    public WahidPlayerTransition(EnemyBase<WahidState> fsm, float range, RangeType type = RangeType.Out) : base(fsm, range, type)
    {
    }

    public WahidPlayerTransition(EnemyBase<WahidState> fsm, float min, float max) : base(fsm, min, max)
    {
    }
}
