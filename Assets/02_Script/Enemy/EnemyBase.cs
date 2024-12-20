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
    [HideInInspector] public TrailRenderer trail;

    public float upgradeTime { get; set; }
    public float minusTime { get; set; }
    public bool isDie { get; set; }
    public bool isStop { get; set; }

    protected virtual void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        trail = GetComponentInChildren<TrailRenderer>();
    }

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
        playerTrs = player.transform;
        playerPos = player.transform.position;
        
        InitializeState();
    }

    protected abstract void InitializeState();

    public virtual void Upgrade()
    {
        sp.material.SetFloat("_On", 1);
        upgradeTime = 0.5f;
    }

    public virtual void Stop(bool value)
    {
        isStop = value;
    }

    public virtual void Death(float minusTime)
    {
        this.minusTime = minusTime;
        isDie = true;
    }

    public Vector2 EnemyPos()
    {
        return transform.position;
    }
}