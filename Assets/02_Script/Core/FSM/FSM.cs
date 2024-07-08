using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T> : MonoBehaviour where T : Enum
{
    protected Dictionary<T, State<T>> stateDictionary = new Dictionary<T, State<T>>();
    protected Dictionary<T, List<Transition<T>>> transitionDictionary = new Dictionary<T, List<Transition<T>>>();
    protected T nowState;

    private void Update()
    {
        stateDictionary[nowState].Update();
    }

    public void AddState(State<T> state, T type)
    {
        if (stateDictionary.ContainsKey(type)) return;

        stateDictionary[type] = state;
    }

    public void ChangeState(T state)
    {
        if (!stateDictionary.ContainsKey(state)) return;
        if ((Enum)nowState == (Enum)state) return;

        stateDictionary[nowState].Exit();
        stateDictionary[nowState = state].Enter();
    }
}
