using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaAttackState : HetaBaseState
{
    public HetaAttackState(HetaFSM fsm) : base(fsm) { }

    protected override void OnStateEnter()
    {
        HetaAttackEffect effect = PoolingManager.Instance.Pop<HetaAttackEffect>(lazerEffect.name, playerTrs.position);
        effect.SetTimeAmount(attackAmount);
        effect.PopEffect(hetaFSM);

        AudioManager.Instance.StartSfx("Lazer_2");
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);
    }
}
