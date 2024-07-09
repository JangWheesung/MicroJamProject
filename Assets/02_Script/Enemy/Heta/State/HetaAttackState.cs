using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaAttackState : HetaBaseState
{
    public HetaAttackState(HetaFSM fsm) : base(fsm) { }

    protected override void OnStateEnter()
    {
        AttackEffectBase effect = PoolingManager.Instance.Pop<AttackEffectBase>(lazerEffect.name, playerTrs.position);
        effect.PopEffect();

        AudioManager.Instance.StartSfx("Lazer_2");
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);
    }

    protected override void OnStateUpdate()
    {

    }

    protected override void OnStateExit()
    {

    }
}
