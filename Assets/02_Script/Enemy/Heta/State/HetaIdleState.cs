using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaIdleState : HetaBaseState
{
    public HetaIdleState(HetaFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateEnter()
    {

    }

    protected override void OnStateUpdate()
    {
        if (!ControlSystem.Instance.IsStopLogic())
        {
            hetaFSM.ChangeState(HetaState.Move);
        }
    }

    protected override void OnStateExit()
    {

    }
}
