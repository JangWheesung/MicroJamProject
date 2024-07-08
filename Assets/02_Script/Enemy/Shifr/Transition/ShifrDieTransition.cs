using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrDieTransition : DieTransition<ShifrState>
{
    public ShifrDieTransition(EnemyBase<ShifrState> fsm) : base(fsm)
    {
    }
}
