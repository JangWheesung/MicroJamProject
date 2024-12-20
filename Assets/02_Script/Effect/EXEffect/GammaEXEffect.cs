using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GammaEXEffect : EXEffectBase
{
    public override void PopEffect(PlayerBase player, bool isParent = false)
    {
        player.transform.DOLocalMove(Vector2.zero, 0.1f).SetEase(Ease.OutExpo);
    }

    public override void UnityAnimEvent()
    {
        AudioManager.Instance.StartSfx("Bullet_1");
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_M);
    }
}
