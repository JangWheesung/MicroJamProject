using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WahidStopState : StopState<WahidState>
{
    public WahidStopState(EnemyBase<WahidState> fsm) : base(fsm)
    {
    }
}
