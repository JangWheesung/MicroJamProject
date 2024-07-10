using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StigmaTimeTransition : TimeTransition<StigmaState>
{
    public StigmaTimeTransition(EnemyBase<StigmaState> fsm, float time) : base(fsm, time)
    {
    }
}
