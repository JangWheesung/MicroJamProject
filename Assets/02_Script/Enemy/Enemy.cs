using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        playerTrs = GameObject.FindWithTag("Player").transform;
        sp = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        //�̰� �� �������� ���ֱ�
        OnAttackEvt += AttackEffectSystem.Instance.CinemachineShaking;
        OnAttackEvt += AttackEffectSystem.Instance.CircleEffect;
        OnAttackEvt += AttackEffectSystem.Instance.SmashingEffect;
    }

    public void AttackEffect()
    {
        OnAttackEvt?.Invoke(playerTrs);
    }
}
