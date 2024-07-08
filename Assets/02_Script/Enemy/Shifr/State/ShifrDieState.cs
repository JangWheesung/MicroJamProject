using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrDieState : DieState<ShifrState>
{
    public ShifrDieState(EnemyBase<ShifrState> fsm) : base(fsm) 
    { 
    }
}
