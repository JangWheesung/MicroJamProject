using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasitPlayerTransition : RangeTransition<HasitState>
{
    public HasitPlayerTransition(HasitFSM fsm, RangeType type = RangeType.Out) : base(fsm, fsm.attackRange, type)
    {
    }
}
