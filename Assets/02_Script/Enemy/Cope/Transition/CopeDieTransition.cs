using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopeDieTransition : DieTransition<CopeState>
{
    public CopeDieTransition(CopeFSM fsm) : base(fsm)
    {
    }
}
