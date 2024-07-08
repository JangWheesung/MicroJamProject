using UnityEngine;
using System;

public abstract class EnemyBase<T> : FSM<T>, IEnemy where T : Enum
{
    public Slice enemySlice;

    [HideInInspector] public PlayerBase player;
    [HideInInspector] public Transform playerTrs;
    [HideInInspector] public Vector2 playerPos;

    [HideInInspector] public SpriteRenderer sp;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

    public bool isDie { get; set; }
    public bool isStop { get; set; }

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
        playerTrs = player.transform;
        playerPos = player.transform.position;

        sp = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        InitializeState();
    }

    protected abstract void InitializeState();
    
    public void Death()
    {
        isDie = true;
    }
}