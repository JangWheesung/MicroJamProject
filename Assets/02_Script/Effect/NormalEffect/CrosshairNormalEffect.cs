using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrosshairNormalEffect : EffectBase
{
    private PlayerBase targetPlayer;
    private bool isPopEffect;

    private void Update()
    {
        if (!isPopEffect) return;

        transform.position = targetPlayer.transform.position;
        //transform.Rotate(Vector2.left, 10f);
    }

    public override void PopEffect(PlayerBase player, bool isParent = false)
    {
        targetPlayer = player;

        transform.rotation = Quaternion.identity;
        DOTween.Sequence()
            .Append(transform.DOScale(Vector2.one, 0.4f))
            .Join(transform.DORotate(new Vector3(0, 0, 180f), 0.4f).SetEase(Ease.OutCirc));

        isPopEffect = true;
        AudioManager.Instance.StartSfx("Lazer_1", 0.9f);
    }
}
