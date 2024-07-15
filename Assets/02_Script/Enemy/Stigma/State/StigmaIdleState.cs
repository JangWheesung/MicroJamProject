using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StigmaIdleState : StigmaBaseState
{
    public StigmaIdleState(StigmaFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateUpdate()
    {
        if (!ControlSystem.Instance.IsStopLogic())
        {
            stigmaFSM.ChangeState(StigmaState.Move);
        }
    }
}
