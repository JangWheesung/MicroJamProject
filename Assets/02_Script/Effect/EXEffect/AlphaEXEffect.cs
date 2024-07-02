using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AlphaEXEffect : EXEffectBase
{
    [SerializeField] private float effectTime;
    private SpriteRenderer sp;

    protected override void OnEnable()
    {
        sp = GetComponent<SpriteRenderer>();

        transform.localScale = Vector2.zero;
        sp.color = Color.red;

        base.OnEnable();
    }

    public override void PopEffect()
    {
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Rock_H, effectTime);

        DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(8, 8, 1), effectTime).SetEase(Ease.Linear))
            .Join(sp.DOColor(Color.white, effectTime))
            .AppendCallback(() => { DisableEffect(); });
    }
}
