using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WahidRangeAttackEffect : BulletAttackEffect
{
    protected override void PlayerHit(PlayerBase player)
    {
        player.Hit(attackAmount);

        if (isPopNormalEffect)
        {
            EffectBase effect = PoolingManager.Instance.Pop<EffectBase>(normalEffect.name, transform.position);
            effect.transform.rotation = transform.rotation;
            effect.PopEffect();
        }

        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_S);

        if (!bulletPenetrate)
            DisableEffect();
    }
}
