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

            AttackEffectBase effect = PoolingManager.Instance.Pop<AttackEffectBase>(meleeAttackEffect.name, wahidFSM.transform.position);
            effect.PopEffect();

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
