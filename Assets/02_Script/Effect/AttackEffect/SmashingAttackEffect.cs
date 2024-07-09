using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashingAttackEffect : AttackEffectBase
{
    [SerializeField] private float attackRadius;

    public override void PopEffect(Vector2 vec)
    {
        transform.up = vec;

        HitRader(transform.position, attackRadius);
    }

    protected override void EnemyHit(IEnemy enemy, Transform enemyTrs)
    {
        base.EnemyHit(enemy, enemyTrs);

        int score = Random.Range(1, 3); //1 ~ 2
        TimeSystem.Instance.PlusTime(score);
    }

    public override void DisableEffect()
    {
        base.DisableEffect();
    }
}
