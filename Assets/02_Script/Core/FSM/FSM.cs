using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FSM_State
{
    Null,
    Any,
    Idle,
    Move,
    Attack,
    Die
};

public class FSM : MonoBehaviour
{
    protected Dictionary<FSM_State, BaseState> stateDictionary = new Dictionary<FSM_State, BaseState>();
    protected BaseState anyState;
    protected FSM_State nowState;

    private void Start()
    {
        anyState = GetComponent<BaseState>();

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
        anyState.OnStateUpdate();
    }

    public void ChangeState(FSM_State state)
    {
        if (nowState == state) return;

        if (nowState != FSM_State.Null)
        {
            stateDictionary[nowState].OnStateExit();
            anyState.OnStateExit();
        }

        nowState = state;

        stateDictionary[nowState].OnStateEnter();
        anyState.OnStateEnter();

    }
}
