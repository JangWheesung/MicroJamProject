using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WahidIdleState : WahidBaseState
{
    public WahidIdleState(WahidFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateUpdate()
    {
        if (!ControlSystem.Instance.IsStopLogic())
        {
            wahidFSM.ChangeState(WahidState.Move);
        }
    }
}
