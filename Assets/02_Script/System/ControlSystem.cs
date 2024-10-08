using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    public static ControlSystem Instance;

    public event Action OnExStartEvt;
    public event Action OnExEndEvt;
    public event Action OnDeathEvt;

    public bool isEX { get; private set; }
    public bool isDeath { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AudioManager.Instance.StartBgm("InGame");
    }

    public void SetEX(bool value)
    {
        isEX = value;

        if (value)
            OnExStartEvt?.Invoke();
        else
            OnExEndEvt?.Invoke();
    }

    public void SetDeath()
    {
        isDeath = true;

        OnDeathEvt?.Invoke();
    }

    public bool IsStopLogic()
    {
        return isDeath || isEX;
    }
}
