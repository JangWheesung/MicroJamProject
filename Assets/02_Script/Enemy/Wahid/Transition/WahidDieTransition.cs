using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WahidDieTransition : DieTransition<WahidState>
{
    public WahidDieTransition(EnemyBase<WahidState> fsm) : base(fsm)
    {
    }
}
