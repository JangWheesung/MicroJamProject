using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrInPlayerTransition : InRangeTransition<ShifrState>
{
    public ShifrInPlayerTransition(ShifrFSM fsm) : base(fsm, fsm.attackRange)
    {
        
    }
}
