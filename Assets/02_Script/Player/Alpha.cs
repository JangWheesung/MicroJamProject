using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha : PlayerBase
{
    protected override void Attack()
    {
        if (!pAttack) return;

        pAttack = false;
        StartCoroutine(AttackDelay());

        Vector2 effectPos = transform.position + (MouseVec() * 2f);

        AttackEffeectBase effect = PoolingManager.Instance.Pop<AttackEffeectBase>(attackEffect.name, effectPos);
        effect.PopEffect(-MouseVec());

        int idx = Random.Range(1, 3);
        AudioManager.Instance.StartSfx($"Smashing_{idx}"); //1 ~ 2
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_M);
    }
}
