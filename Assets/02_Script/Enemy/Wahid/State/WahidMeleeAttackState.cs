using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WahidMeleeAttackState : WahidBaseState
{
    private float currentTime;

    public WahidMeleeAttackState(WahidFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateEnter()
    {
        sp.flipX = false;
    }

    protected override void OnStateUpdate()
    {
        if (currentTime <= 0)
        {
            wahidFSM.transform.right = LookPlayerVec();

            WahidMeleeAttackEffect effect = PoolingManager.Instance.Pop<WahidMeleeAttackEffect>(meleeAttackEffect.name, wahidFSM.transform.position);
            effect.SetTimeAmount(attackAmount);
            effect.PopEffect(wahidFSM);

            SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_S);
            AudioManager.Instance.StartSfx("Circle");

            currentTime = meleeDelay;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    protected override void OnStateExit()
    {
        currentTime = 0;
    }
}
