using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopeBombState : CopeBaseState
{
    public CopeBombState(CopeFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateEnter()
    {
        CopeAttackEffect effect = PoolingManager.Instance.Pop<CopeAttackEffect>(bombEffect.name, copeFSM.transform.position);
        effect.SetTimeAmount(attackAmount);
        effect.PopEffect(copeFSM);

        //AudioManager.Instance.StartSfx("Lazer_2");
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);

        copeFSM.ChangeState(CopeState.Die);
    }
}
