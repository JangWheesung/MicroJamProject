using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeltaEXEffect : EXEffectBase
{
    PlayerBase player;

    public override void PopEffect(PlayerBase player, bool isParent = false)
    {
        this.player = player;

        Vector3 playerPos = new Vector3(0, -3.9f, 0);
        player.transform.DOLocalMove(playerPos, 0.1f).SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                player.SetSpriteVisuality(false);

                //SpecialEffectSystem.Instance.CameraShaking(CameraType.ZoomIn);
            });
    }

    public override void DisableEffect()
    {
        player.SetSpriteVisuality(true);
        base.DisableEffect();
    }

    public override void UnityAnimEvent()
    {
        SpecialEffectSystem.Instance.BackgroundAura(Color.green);
    }

    public void UnityAnimEvent_1()
    {
        AudioManager.Instance.StartSfx("Punch");
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);
    }

    public void UnityAnimEvent_2()
    {
        AudioManager.Instance.StartSfx("DeltaEX");
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H, 1.2f);
    }
}
