using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaDieState : DieState<HetaState>
{
    public HetaDieState(EnemyBase<HetaState> fsm) : base(fsm)
    {
    }
}
