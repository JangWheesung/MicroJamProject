using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasitAttackState : HasitBaseState
{
    private float currentTime;

    public HasitAttackState(HasitFSM fsm) : base(fsm) { }

    protected override void OnStateUpdate()
    {
        if (currentTime <= 0)
        {
            AttackEffectBase effect = PoolingManager.Instance.Pop<AttackEffectBase>(circleEffect.name, hasitFSM.transform.position);
            effect.SetTimeAmount(attackAmount);
            effect.PopEffect();

            SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_M);
            AudioManager.Instance.StartSfx("Circle");

            currentTime = attackDelay;
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
