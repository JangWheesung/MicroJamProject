using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BetaEXEffect : EXEffectBase
{
    public override void PopEffect(PlayerBase player)
    {
        player.transform.DOLocalMove(Vector2.zero, 0.1f).SetEase(Ease.OutExpo);
    }

    public override void UnityAnimEvent()
    {
        AudioManager.Instance.StartSfx("BetaEX");
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);
    }
}