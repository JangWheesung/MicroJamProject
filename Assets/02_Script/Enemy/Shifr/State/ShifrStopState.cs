using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrStopState : StopState<ShifrState>
{
    public ShifrStopState(EnemyBase<ShifrState> fsm) : base(fsm) 
    { 
    }
}
