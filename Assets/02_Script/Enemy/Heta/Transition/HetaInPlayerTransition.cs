using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaInPlayerTransition : InRangeTransition<HetaState>
{
    public HetaInPlayerTransition(HetaFSM fsm) : base(fsm, fsm.attackRange)
    {
    }
}
