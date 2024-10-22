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
            ShifrAttackEffect effect = PoolingManager.Instance.Pop<ShifrAttackEffect>(circleEffect.name, shifrFSM.transform.position);
            effect.SetTimeAmount(attackAmount);
            effect.PopEffect(shifrFSM);

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
