using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : BaseState
{
    [SerializeField] private float breakPower;
    [SerializeField] private Slice sliceObj;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnStateEnter()
    {
        Slice slices = PoolingManager.instance.Pop<Slice>(sliceObj.name, transform.position);
        slices.BreakEffect(breakPower);
    }

    public override void OnStateExit()
    {
        PoolingManager.instance.Push(enemy.gameObject);
    }

    public override void OnStateUpdate()
    {
        fsm.ChangeState(FSM_State.Idle);
    }

    protected override void OnStateAction()
    {
        
    }
}
