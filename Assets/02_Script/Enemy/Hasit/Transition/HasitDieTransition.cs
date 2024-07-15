using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasitDieTransition : DieTransition<HasitState>
{
    public HasitDieTransition(EnemyBase<HasitState> fsm) : base(fsm)
    {
    }
}
