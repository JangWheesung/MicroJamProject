using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasitDieState : DieState<HasitState>
{
    public HasitDieState(EnemyBase<HasitState> fsm) : base(fsm)
    {
    }
}
