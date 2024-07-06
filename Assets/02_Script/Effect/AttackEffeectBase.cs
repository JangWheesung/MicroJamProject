using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffeectBase : EffectBase
{
    [Header("AttackBase")]
    [SerializeField] protected EffectBase normalEffect;
    [SerializeField] private LayerMask enemyLayer;

    public override void PopEffect() { }
    public virtual void PopEffect(Vector2 vec) { }
    public virtual void PopEffect(object value) { }

    protected void EnemyRader(Vector2 pos, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<EnemyBase>(out EnemyBase enemy))
            {
                EnemyHit(enemy);
            }
        }
    }

    protected virtual void EnemyHit(EnemyBase enemy)
    {
        enemy.Death();

        EffectBase effect = PoolingManager.Instance.Pop<EffectBase>(normalEffect.name, enemy.transform.position);
        effect.PopEffect();

        UISystem.Instance.GetKillCount();
    }
}
