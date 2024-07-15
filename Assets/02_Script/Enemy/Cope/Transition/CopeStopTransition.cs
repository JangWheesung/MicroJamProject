using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopeStopTransition : StopTransition<CopeState>
{
    public CopeStopTransition(CopeFSM fsm) : base(fsm)
    {
    }
}
