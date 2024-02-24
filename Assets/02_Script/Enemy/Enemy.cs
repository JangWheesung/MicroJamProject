using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
    }

    public void AttackEffect() //공격효과 (이건 채성이가 만들면 복붙해서 사용)
    {
        
    }
}
