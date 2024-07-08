using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifrAttackState : ShifrBaseState
{
    private float currentTime;

    public ShifrAttackState(ShifrFSM fsm) : base(fsm) { }

    protected override void OnStateEnter()
    {
        
    }

    protected override void OnStateUpdate()
    {
        if (currentTime <= 0)
        {
            AttackEffeectBase effect = PoolingManager.Instance.Pop<AttackEffeectBase>(circleEffect.name, wowFSM.transform.position);
            effect.PopEffect();

            player.Hit();

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
