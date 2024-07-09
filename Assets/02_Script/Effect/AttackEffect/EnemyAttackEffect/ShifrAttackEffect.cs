using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShifrAttackEffect : AttackEffectBase
{
    [SerializeField] private float scaleSize;
    private SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public override void PopEffect()
    {
        HitRader(transform.position, scaleSize);

        transform.DOScale(new Vector3(scaleSize, scaleSize, 1), 0.5f).SetEase(Ease.OutElastic).OnComplete(() =>
        {
            sp.DOFade(0.2f, 0.2f).OnComplete(() =>
            {
                DisableEffect();
            });
        });
    }

    protected override void PlayerHit(PlayerBase player)
    {
        player.Hit();

        TimeSystem.Instance.MinusTime(5);
    }

    protected override void EnemyHit(IEnemy enemy, Transform enemyTrs)
    {
        enemy.Death();

        UISystem.Instance.GetKillCount();
    }

    public override void DisableEffect()
    {
        Color color = new Color(1, 1, 1, 1);
        sp.color = color;
        transform.localScale = Vector3.one;

        base.DisableEffect();
    }
}
