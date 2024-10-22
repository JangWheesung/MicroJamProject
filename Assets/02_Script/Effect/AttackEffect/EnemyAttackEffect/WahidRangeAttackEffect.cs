using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WahidRangeAttackEffect : BulletAttackEffect
{
    public void PopEffect(Vector2 vec, IEnemy enemy)
    {
        base.PopEffect(enemy);
        base.PopEffect(vec);
    }

    protected override void PlayerHit(PlayerBase player)
    {
        player.Hit(ownerAttackEnemy, timeAmount);

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
