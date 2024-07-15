using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState<T> : BaseState<T> where T : Enum
{
    private EnemyBase<T> enemyFSM;
    private float breakPower = 3f;

    public DieState(EnemyBase<T> fsm) : base(fsm) 
    {
        enemyFSM = fsm;
    }

    protected override void OnStateEnter()
    {
        Slice slices = PoolingManager.Instance.Pop<Slice>(enemySlice.name, enemyBase.transform.position);
        slices.BreakEffect(breakPower);

        TimeSystem.Instance.PlusTime(enemyFSM.minusTime);
        UISystem.Instance.GetKillCount();
        AudioManager.Instance.StartSfx("EnemyDeath");
        PoolingManager.Instance.Push(enemyBase.gameObject);
    }

    protected override void OnStateUpdate()
    {

    }

    protected override void OnStateExit()
    {
        enemyBase.isDie = false;
    }
}
