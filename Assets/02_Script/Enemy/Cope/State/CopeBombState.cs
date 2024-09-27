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
        AttackEffectBase effect = PoolingManager.Instance.Pop<AttackEffectBase>(bombEffect.name, copeFSM.transform.position);
        effect.SetTimeAmount(attackAmount);
        effect.PopEffect();

        //AudioManager.Instance.StartSfx("Lazer_2");
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);

        copeFSM.ChangeState(CopeState.Die);
    }
}
