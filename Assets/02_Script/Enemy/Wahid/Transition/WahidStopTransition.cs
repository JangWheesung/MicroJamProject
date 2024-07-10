using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WahidStopTransition : StopTransition<WahidState>
{
    public WahidStopTransition(EnemyBase<WahidState> fsm) : base(fsm)
    {
    }
}
