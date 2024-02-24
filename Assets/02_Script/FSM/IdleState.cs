using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    bool isStop; //게임 중 일시정지일때를 고려해 임시선언

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnStateEnter()
    {
        Debug.Log("아이들");
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        if (!isStop)
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
