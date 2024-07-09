using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaTimeTransition : TimeTransition<HetaState>
{
    public HetaTimeTransition(EnemyBase<HetaState> fsm, float time) : base(fsm, time)
    {
    }
}
