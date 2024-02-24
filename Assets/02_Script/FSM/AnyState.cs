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

    public override void OnStateUpdate() //�Ͻ������ɋ�, ���϶�, ���ӿ��� ��
    {
        
    }

    protected override void OnStateAction()
    {
        
    }
}
