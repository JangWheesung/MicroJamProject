using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> where T : Enum
{
    protected FSM<T> controller;

    public State(FSM<T> fsm)
    {
        controller = fsm;
    }

    public virtual void OnStateEnter()
    {

    }

    public virtual void OnStateUpdate()
    {

    }

    public virtual void OnStateExit()
    {

    }

    protected TCompo GetComponent<TCompo>()
    {
        return controller.GetComponent<TCompo>();
    }

    protected Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return controller.StartCoroutine(coroutine);
    }

    protected void StopCoroutine(Coroutine coroutine)
    {
        controller.StopCoroutine(coroutine);
    }
}
