using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrPlayerTransition : RangeTransition<ShifrState>
{
    public ShifrPlayerTransition(ShifrFSM fsm, RangeType type) : base(fsm, fsm.attackRange, type)
    {
    }

    public ShifrPlayerTransition(ShifrFSM fsm, float min, float max) : base(fsm, min, max)
    {
    }
}
