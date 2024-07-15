using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasitIdleState : HasitBaseState
{
    public HasitIdleState(HasitFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateUpdate()
    {
        if (!ControlSystem.Instance.IsStopLogic())
        {
            hasitFSM.ChangeState(HasitState.Move);
        }
    }
}
