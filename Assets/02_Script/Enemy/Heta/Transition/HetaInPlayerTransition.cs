using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaInPlayerTransition : RangeTransition<HetaState>
{
    public HetaInPlayerTransition(HetaFSM fsm, RangeType type) : base(fsm, fsm.attackRange, type)
    {
    }

    public HetaInPlayerTransition(HetaFSM fsm, float min, float max) : base(fsm, min, max)
    {
    }
}
