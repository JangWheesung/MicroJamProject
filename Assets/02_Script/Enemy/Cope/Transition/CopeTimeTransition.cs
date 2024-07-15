using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopeTimeTransition : TimeTransition<CopeState>
{
    public CopeTimeTransition(EnemyBase<CopeState> fsm, float time) : base(fsm, time)
    {
    }
}
