using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition<T> where T : Enum
{
    protected FSM<T> controller;
    public T changeStateType { get; set; }

    public Transition(FSM<T> fsm)
    {
        controller = fsm;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual bool CheckTransition()
    {
        return false;
    }

    public void SetChangeState(T state)
    {
        changeStateType = state;
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
