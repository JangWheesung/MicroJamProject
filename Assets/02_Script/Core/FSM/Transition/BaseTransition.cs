using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTransition<T> : Transition<T> where T : Enum
{
    protected EnemyBase<T> enemyBase;

    protected Slice enemySlice;

    protected PlayerBase player;
    protected Transform playerTrs;
    protected Vector2 playerPos;

    protected SpriteRenderer sp;
    protected Rigidbody2D rb;
    protected Collider2D col;

    public BaseTransition(EnemyBase<T> fsm) : base(fsm)
    {
        enemyBase = fsm;

        enemySlice = fsm.enemySlice;

        player = fsm.player;
        playerTrs = fsm.playerTrs;
        playerPos = fsm.playerPos;

        sp = fsm.sp;
        rb = fsm.rb;
        col = fsm.col;
    }
}
