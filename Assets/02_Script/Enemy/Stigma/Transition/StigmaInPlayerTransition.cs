using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StigmaInPlayerTransition : RangeTransition<StigmaState> 
{
    public StigmaInPlayerTransition(StigmaFSM fsm, RangeType type) : base(fsm, fsm.attackRange, type)
    {
    }

    public StigmaInPlayerTransition(StigmaFSM fsm, float min, float max) : base(fsm, min, max)
    {
    }
}
