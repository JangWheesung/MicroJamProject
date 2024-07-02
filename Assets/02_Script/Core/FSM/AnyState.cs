using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyState : BaseState
{
    protected override void Awake()
    {
        fsm = GetComponent<FSM>();
        enemy = fsm.GetComponentInParent<Enemy>();
    }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate() //일시정지될떄
    {
        if (enemy.isDie)
        {
            fsm.ChangeState(FSM_State.Die);
        }

        if (GameSystem.Instance.IsStopLogic())
        {
            fsm.ChangeState(FSM_State.Idle);
        }
    }

    protected override void OnStateAction()
    {
        
    }
}
