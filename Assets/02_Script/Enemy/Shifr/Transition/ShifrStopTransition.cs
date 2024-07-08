using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrStopTransition : StopTransition<ShifrState>
{
    public ShifrStopTransition(EnemyBase<ShifrState> fsm) : base(fsm)
    {
    }
}
