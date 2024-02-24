using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    bool isStop; //���� �� �Ͻ������϶��� ����� �ӽü���

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnStateEnter()
    {
        Debug.Log("���̵�");
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
