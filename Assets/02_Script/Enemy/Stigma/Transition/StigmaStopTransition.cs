using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StigmaStopTransition : StopTransition<StigmaState>
{
    public StigmaStopTransition(EnemyBase<StigmaState> fsm) : base(fsm)
    {
    }
}
