using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTransition<T> : Transition<T> where T : Enum
{
    private EnemyBase<T> enemyBase;

    private float checkTime;
    private float currentTime;

    public TimeTransition(EnemyBase<T> fsm, float time) : base(fsm)
    {
        enemyBase = fsm;
        checkTime = time;
    }

    public override void Enter()
    {
        currentTime = 0f;
    }

    public override bool CheckTransition()
    {
        currentTime += Time.deltaTime;
        return currentTime >= checkTime;
    }
}
