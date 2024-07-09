using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaDieTransition : DieTransition<HetaState>
{
    public HetaDieTransition(EnemyBase<HetaState> fsm) : base(fsm)
    {
    }
}
