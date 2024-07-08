using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStorage<T> where T : Enum
{
    private State<T>[] states;

    public StateStorage(params State<T>[] states)
    {
        this.states = states;
    }

    public void AddTransition(Transition<T> transition, T changeState)
    {
        foreach (State<T> state in states)
        {
            state.AddTransition(transition, changeState);
        }
    }
}
