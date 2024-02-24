using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    [SerializeField] protected float radius;

    protected Transform playerTrs;
    protected SpriteRenderer sp;
    protected Rigidbody2D rb;
    protected float jumpPower;
    protected float moveSpeed;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnStateEnter()
    {
        playerTrs = enemy.playerTrs;
        sp = enemy.sp;
        rb = enemy.rb;
        jumpPower = enemy.jumpPower;
        moveSpeed = enemy.moveSpeed;
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        if (Vector2.Distance(transform.position, playerTrs.position) <= radius)
        {
            fsm.ChangeState(FSM_State.Attack);
        }
        else
        {
            OnStateAction();
        }
    }

    protected override void OnStateAction()
    {
        float distance = Mathf.Clamp(playerTrs.position.x - transform.position.x, -1, 1);
        rb.velocity = new Vector2(distance * moveSpeed, rb.velocity.y);
        sp.flipX = distance == -1 ? true : false;

        if (Mathf.Abs(rb.velocity.y) == 0)
        {
            Debug.Log("มกวม");
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}
