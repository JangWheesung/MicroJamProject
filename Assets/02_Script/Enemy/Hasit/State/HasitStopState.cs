using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasitStopState : StopState<HasitState>
{
    public HasitStopState(EnemyBase<HasitState> fsm) : base(fsm)
    {
    }
}
