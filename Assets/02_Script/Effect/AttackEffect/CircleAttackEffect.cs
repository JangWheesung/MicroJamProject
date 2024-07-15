using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleAttackEffect : AttackEffectBase
{
    [SerializeField] protected float scaleSize;
    protected SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public override void PopEffect()
    {
        HitRader(transform.position, scaleSize);
        CircleEffect();
    }

    public override void PopEffect(PlayerBase player)
    {
        HitRader(player.transform.position, scaleSize);
        CircleEffect();
    }

    private void CircleEffect()
    {
        transform.DOScale(new Vector3(scaleSize, scaleSize, 1), 0.5f).SetEase(Ease.OutElastic).OnComplete(() =>
        {
            sp.DOFade(0.2f, 0.2f).OnComplete(() =>
            {
                DisableEffect();
            });
        });
    }

    public override void DisableEffect()
    {
        Color color = new Color(1, 1, 1, 1);
        sp.color = color;
        transform.localScale = Vector3.one;

        base.DisableEffect();
    }
}
