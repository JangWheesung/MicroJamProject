using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrOutPlayerTransition : OutRangeTransition<ShifrState>
{
    public ShifrOutPlayerTransition(ShifrFSM fsm) : base(fsm, fsm.attackRange)
    {
    }
}
