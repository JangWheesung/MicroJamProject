using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrAttackState : ShifrBaseState
{
    private float currentTime;

    public ShifrAttackState(ShifrFSM fsm) : base(fsm) { }

    protected override void OnStateUpdate()
    {
        if (currentTime <= 0)
        {
            AttackEffectBase effect = PoolingManager.Instance.Pop<AttackEffectBase>(circleEffect.name, shifrFSM.transform.position);
            effect.PopEffect();

            SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_S);
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
