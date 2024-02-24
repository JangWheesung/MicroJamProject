using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public FSM_State state;

    private FSM fsm;

    protected virtual void Awake()
    {
        fsm = transform.parent.GetComponent<FSM>();
    }

    protected abstract void OnStateAction(); //�� �ൿ

    public abstract void OnStateEnter(); //�� ���°� �����Ҷ�
    public abstract void OnStateUpdate(); //���¹ٲ��� ���
    public abstract void OnStateExit(); //�� ���°� ������
}
