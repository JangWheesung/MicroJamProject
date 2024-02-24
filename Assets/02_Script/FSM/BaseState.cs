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

    protected abstract void OnStateAction(); //할 행동

    public abstract void OnStateEnter(); //이 상태가 시작할때
    public abstract void OnStateUpdate(); //상태바뀜을 담당
    public abstract void OnStateExit(); //이 상태가 떠날때
}
