using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Enemy : MonoBehaviour
{
    public event Action<Transform> OnAttackEvt;

    [HideInInspector] public Transform playerTrs;
    [HideInInspector] public SpriteRenderer sp;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

    public float jumpPower;
    public float moveSpeed;
    public float attackDelay;

    public bool isDie { get; private set; }

    private void OnEnable()
    {
        isDie = false;

        OnAttackEvt += AttackEffectSystem.Instance.CinemachineShaking;
        OnAttackEvt += AttackEffectSystem.Instance.CircleEffect;
        OnAttackEvt += AttackEffectSystem.Instance.SmashingEffect;
    }

    private void Start()
    {
        playerTrs = GameObject.FindWithTag("Player").transform;
        sp = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void AttackEffect()
    {
        OnAttackEvt?.Invoke(playerTrs);
    }

    public void Death()
    {
        isDie = true;
    }

    private void OnDisable()
    {
        OnAttackEvt -= AttackEffectSystem.Instance.CinemachineShaking;
        OnAttackEvt -= AttackEffectSystem.Instance.CircleEffect;
        OnAttackEvt -= AttackEffectSystem.Instance.SmashingEffect;
    }
}
