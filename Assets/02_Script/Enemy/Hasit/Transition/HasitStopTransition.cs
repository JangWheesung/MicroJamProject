using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasitStopTransition : StopTransition<HasitState>
{
    public HasitStopTransition(EnemyBase<HasitState> fsm) : base(fsm)
    {
    }
}
