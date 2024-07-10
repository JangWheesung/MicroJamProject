using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StigmaDieState : DieState<StigmaState>
{
    private StigmaFSM stigmaFSM;

    public StigmaDieState(StigmaFSM fsm) : base(fsm)
    {
        stigmaFSM = fsm;
    }

    protected override void OnStateEnter()
    {
        if (Random.Range(0, 10f) < 2f)
        {
            stigmaFSM.ChangeState(StigmaState.Fade);
        }
        else
        {
            base.OnStateEnter();
        }
    }
}
