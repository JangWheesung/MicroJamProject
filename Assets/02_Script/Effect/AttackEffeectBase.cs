using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffeectBase : EffectBase
{
    [Header("AttackBase")]
    [SerializeField] protected EffectBase normalEffect;
    [SerializeField] private LayerMask hitLayer;

    public override void PopEffect() { }
    public virtual void PopEffect(Vector2 vec) { }
    public virtual void PopEffect(object value) { }

    protected void HitRader(Vector2 pos, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, radius, hitLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out PlayerBase player))
            {
                PlayerHit(player);
            }

            if (collider.TryGetComponent(out IEnemy enemy))
            {
                EnemyHit(enemy, collider.transform);
            }
        }
    }

    protected virtual void PlayerHit(PlayerBase player)
    {
        player.Hit();

        //EffectBase effect = PoolingManager.Instance.Pop<EffectBase>(normalEffect.name, player.transform.position);
        //effect.PopEffect();
    }

    protected virtual void EnemyHit(IEnemy enemy, Transform enemyTrs)
    {
        enemy.Death();

        EffectBase effect = PoolingManager.Instance.Pop<EffectBase>(normalEffect.name, enemyTrs.position);
        effect.PopEffect();

        UISystem.Instance.GetKillCount();
    }
}
