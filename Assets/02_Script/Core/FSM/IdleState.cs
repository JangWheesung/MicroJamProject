using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        if (!GameoverSystem.Instance.isDeath)
        {
            fsm.ChangeState(FSM_State.Move);
        }
        else
        {
            OnStateAction();
        }
    }

    protected override void OnStateAction()
    {
        
    }
}
