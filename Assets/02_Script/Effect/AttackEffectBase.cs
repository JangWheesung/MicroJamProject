using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectBase : EffectBase
{
    [Header("AttackBase")]
    [SerializeField] protected EffectBase normalEffect;
    [SerializeField] private LayerMask hitLayer;

    protected void HitRader(Vector3 pos, Vector3 size, float angle)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos, size, angle, hitLayer);
        CatchObject(colliders);
    }

    protected void HitRader(Vector3 pos, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, radius, hitLayer);
        CatchObject(colliders);
    }

    private void CatchObject(Collider2D[] colliders)
    {
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
