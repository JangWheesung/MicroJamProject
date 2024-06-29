using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashingAttackEffect : AttackEffeectBase
{
    [SerializeField] private float attackRadius;

    public override void PopEffect()
    {
        EnemyRader(transform.position, attackRadius);
    }

    protected override void EnemyHit(Enemy enemy)
    {
        enemy.Death();

        NormalEffectBase effect = PoolingManager.instance.Pop<NormalEffectBase>(normalEffect.name, enemy.transform.position);
        effect.PopEffect();

        GameoverSystem.Instance.GetKillCount();

        int score = Random.Range(1, 3); //1~2
        TimeSystem.Instance.PlusTime(score);
    }

    protected override void DisableEffect()
    {
        base.DisableEffect();
    }
}
