using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopState<T> : BaseState<T> where T : Enum
{
    private float originGravity;

    public StopState(EnemyBase<T> fsm) : base(fsm) { }

    protected override void OnStateEnter()
    {
        enemyBase.isStop = true;

        originGravity = enemyBase.rb.gravityScale;

        enemyBase.rb.velocity = Vector2.zero;
        enemyBase.rb.gravityScale = 0f;
    }

    protected override void OnStateUpdate()
    {

    }

    protected override void OnStateExit()
    {
        enemyBase.isStop = false;

        enemyBase.rb.gravityScale = originGravity;
    }
}
