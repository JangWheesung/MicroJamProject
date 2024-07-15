using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState<T> : State<T> where T : Enum
{
    protected EnemyBase<T> enemyBase;

    protected Slice enemySlice;

    protected PlayerBase player;
    protected Transform playerTrs;

    protected SpriteRenderer sp;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected TrailRenderer trail;

    public BaseState(EnemyBase<T> fsm) : base(fsm)
    {
        enemyBase = fsm;

        enemySlice = fsm.enemySlice;

        player = fsm.player;
        playerTrs = fsm.playerTrs;

        sp = fsm.sp;
        rb = fsm.rb;
        col = fsm.col;
        trail = fsm.trail;
    }

    protected Vector3 LookPlayerVec()
    {
        return (playerTrs.position - enemyBase.transform.position).normalized;
    }
}
