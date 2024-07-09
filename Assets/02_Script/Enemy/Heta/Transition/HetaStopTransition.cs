using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaStopTransition : StopTransition<HetaState>
{
    public HetaStopTransition(EnemyBase<HetaState> fsm) : base(fsm)
    {
    }
}
