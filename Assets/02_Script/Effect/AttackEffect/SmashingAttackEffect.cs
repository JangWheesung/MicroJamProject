using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashingAttackEffect : AttackEffeectBase
{
    [SerializeField] private float attackRadius;

    public override void PopEffect(Vector2 vec)
    {
        transform.up = vec;

        EnemyRader(transform.position, attackRadius);
    }

    protected override void EnemyHit(Enemy enemy)
    {
        base.EnemyHit(enemy);

        int score = Random.Range(1, 3); //1 ~ 2
        TimeSystem.Instance.PlusTime(score);
    }

    protected override void DisableEffect()
    {
        base.DisableEffect();
    }
}
