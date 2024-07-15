using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopePlayerTransition : RangeTransition<CopeState>
{
    public CopePlayerTransition(CopeFSM fsm) : base(fsm, fsm.attackRange, RangeType.In)
    {
    }
}
