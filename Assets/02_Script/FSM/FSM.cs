using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FSM_State
{
    Null,
    Idle,
    Move,
    Attack
};

public class FSM : MonoBehaviour
{
    protected Dictionary<FSM_State, BaseState> stateDictionary;
    protected FSM_State nowState;

    private void Awake()
    {
        foreach (BaseState state in GetComponentsInChildren<BaseState>())
        {
            stateDictionary.Add(state.state, state);
        }

        nowState = FSM_State.Null;
        ChangeState(FSM_State.Idle);
    }

    private void Update()
    {
        stateDictionary[nowState].OnStateUpdate();
    }

    public void ChangeState(FSM_State state)
    {
        if (nowState == state) return;

        if(nowState != FSM_State.Null)
            stateDictionary[nowState].OnStateExit();

        nowState = state;

        stateDictionary[nowState].OnStateEnter();
    }
}
