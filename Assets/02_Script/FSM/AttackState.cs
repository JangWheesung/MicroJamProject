using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    [SerializeField] protected float radius;
    protected float currentTime;

    protected Transform playerTrs;
    protected float attackDelay;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnStateEnter()
    {
        playerTrs = enemy.playerTrs;
        attackDelay = enemy.attackDelay;
    }

    public override void OnStateExit()
    {
        //currentTime = 0;
    }

    public override void OnStateUpdate()
    {
        if (Vector2.Distance(transform.position, playerTrs.position) > radius)
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
        if (currentTime <= 0)
        {
            TimeSystem.Instance.MinusTime(1);
            enemy.AttackEffect();
            currentTime = attackDelay;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }
}
