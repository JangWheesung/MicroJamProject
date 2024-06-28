using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseState : MonoBehaviour
{
    public FSM_State state;

    protected FSM fsm;
    protected Enemy enemy;

    protected virtual void Awake()
    {
        fsm = GetComponentInParent<FSM>();
        enemy = fsm.GetComponentInParent<Enemy>();
    }

    protected abstract void OnStateAction(); //�� �ൿ

    public abstract void OnStateEnter(); //�� ���°� �����Ҷ�
    public abstract void OnStateUpdate(); //���¹ٲ��� ���
    public abstract void OnStateExit(); //�� ���°� ������
}
