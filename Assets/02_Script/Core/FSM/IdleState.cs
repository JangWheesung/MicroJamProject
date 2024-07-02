using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    protected Rigidbody2D rb;
    float originGravity = 3f;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnStateEnter()
    {
        rb = enemy.rb;

        originGravity = rb.gravityScale;
    }

    public override void OnStateExit()
    {
        rb.gravityScale = originGravity;
    }

    public override void OnStateUpdate()
    {
        if (!ControlSystem.Instance.IsStopLogic())
        {
            fsm.ChangeState(FSM_State.Move);
        }
        else
        {
            OnStateAction();
        }
    }

    protected override void OnStateAction()
    {
        if (ControlSystem.Instance.isEX)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
    }
}
