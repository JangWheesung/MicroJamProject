using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaStopState : StopState<HetaState>
{
    public HetaStopState(EnemyBase<HetaState> fsm) : base(fsm)
    {
    }
}
