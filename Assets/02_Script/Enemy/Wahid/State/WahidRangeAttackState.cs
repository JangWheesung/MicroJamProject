using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WahidRangeAttackState : WahidBaseState
{
    private float currentTime;

    public WahidRangeAttackState(WahidFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateEnter()
    {
        sp.flipX = false;
    }

    protected override void OnStateUpdate()
    {
        wahidFSM.transform.right = LookPlayerVec();

        if (currentTime <= 0)
        {
            AttackEffectBase effect = PoolingManager.Instance.Pop<AttackEffectBase>(rangeAttackEffect.name, wahidFSM.transform.position);
            effect.PopEffect(LookPlayerVec());

            AudioManager.Instance.StartSfx("Bullet_2", 0.8f);
            SpecialEffectSystem.Instance.CameraShaking(CameraType.Rock_S);

            wahidFSM.transform.right = LookPlayerVec();
            currentTime = rangeDelay;
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