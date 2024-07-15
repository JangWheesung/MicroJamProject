using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrIdleState : ShifrBaseState
{
    public ShifrIdleState(ShifrFSM fsm) : base(fsm) { }

    protected override void OnStateUpdate()
    {
        if (!ControlSystem.Instance.IsStopLogic())
        {
            shifrFSM.ChangeState(ShifrState.Move);
        }
    }
}
