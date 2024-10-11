using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTransition<T> : Transition<T> where T : Enum
{
    private EnemyBase<T> enemyBase;

    public StopTransition(EnemyBase<T> fsm) : base(fsm)
    {
        enemyBase = fsm;
    }

    public override bool CheckTransition()
    {
        return ControlSystem.Instance.IsStopLogic() || enemyBase.isStop;
    }
}
