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

    protected abstract void OnStateAction(); //할 행동

    public abstract void OnStateEnter(); //이 상태가 시작할때
    public abstract void OnStateUpdate(); //상태바뀜을 담당
    public abstract void OnStateExit(); //이 상태가 떠날때
}
