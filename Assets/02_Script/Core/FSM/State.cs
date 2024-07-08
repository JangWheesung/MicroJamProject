using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : Enum
{
    protected List<Transition<T>> transitionDictionary = new List<Transition<T>>();
    protected FSM<T> controller;

    public State(FSM<T> fsm)
    {
        controller = fsm;
    }

    public void Enter()
    {
        OnStateEnter();

        foreach (var transition in transitionDictionary)
        {
            transition.Enter();
        }
    }

    public void Update()
    {
        OnStateUpdate();

        foreach (var transition in transitionDictionary)
        {
            if (transition.CheckTransition())
            {
                controller.ChangeState(transition.changeStateType);
                break;
            }
        }
    }

    public void Exit()
    {
        OnStateExit();

        foreach (var transition in transitionDictionary)
        {
            transition.Exit();
        }
    }

    protected virtual void OnStateEnter() { }

    protected virtual void OnStateUpdate() { }

    protected virtual void OnStateExit() { }

    public void AddTransition(Transition<T> transition, T changeState)
    {
        transition.SetChangeState(changeState);
        transitionDictionary.Add(transition);
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
