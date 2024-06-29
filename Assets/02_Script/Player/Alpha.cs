using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha : PlayerBase
{
    protected override void Attack()
    {
        if (!isAttacking)
        {
            int idx = Random.Range(1, 3);
            AudioManager.Instance.StartSfx($"Smashing{idx}"); //1 ~ 2

            isAttacking = true;
            StartCoroutine(AttackDelay());
            
            Vector2 effectPos = transform.position + (MouseVec() * 2f);

            AttackEffeectBase effect = PoolingManager.instance.Pop<AttackEffeectBase>(attackEffect.name, effectPos);
            effect.transform.up = -MouseVec();
            effect.PopEffect();

            CinemachineEffectSystem.Instance.CinemachineShaking();
        }
    }
}
