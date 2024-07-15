using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopeIdleState : CopeBaseState
{
    public CopeIdleState(CopeFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateUpdate()
    {
        if (!ControlSystem.Instance.IsStopLogic())
        {
            copeFSM.ChangeState(CopeState.Move);
        }
    }
}
