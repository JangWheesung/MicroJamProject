using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrIdleState : ShifrBaseState
{
    public ShifrIdleState(ShifrFSM fsm) : base(fsm) { }

    protected override void OnStateEnter()
    {
        
    }

    protected override void OnStateUpdate()
    {
        if (!ControlSystem.Instance.IsStopLogic())
        {
            wowFSM.ChangeState(ShifrState.Move);
        }
    }

    protected override void OnStateExit()
    {
        
    }
}
