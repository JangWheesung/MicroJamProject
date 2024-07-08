using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTransition<T> : Transition<T> where T : Enum
{
    private EnemyBase<T> enemyBase;

    public DieTransition(EnemyBase<T> fsm) : base(fsm)
    {
        enemyBase = fsm;
    }

    public override bool CheckTransition()
    {
        return enemyBase.isDie;
    }
}
