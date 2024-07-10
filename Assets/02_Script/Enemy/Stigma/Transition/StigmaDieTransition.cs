using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StigmaDieTransition : DieTransition<StigmaState>
{
    public StigmaDieTransition(EnemyBase<StigmaState> fsm) : base(fsm)
    {
    }
}
