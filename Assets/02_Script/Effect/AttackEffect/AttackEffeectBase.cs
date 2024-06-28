using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffeectBase : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected NormalEffectBase normalEffect;
    [SerializeField] private float liveTime;
    [SerializeField] private LayerMask enemyLayer;

    private Coroutine lifeCor;

    private void OnEnable()
    {
        lifeCor = StartCoroutine(LifeEffectCor());
    }

    public virtual void PopEffect()
    {
        EnemyRader(transform.position, 0f);
    }

    protected void EnemyRader(Vector2 pos, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                EnemyHit(enemy);
            }
        }
    }

    protected virtual void EnemyHit(Enemy enemy)
    {
        enemy.Death();

        NormalEffectBase effect = PoolingManager.instance.Pop<NormalEffectBase>(normalEffect.name, enemy.transform.position);
        effect.PopEffect();
    }

    protected virtual void DisableEffect()
    {
        if (lifeCor != null)
            StopCoroutine(lifeCor);

        PoolingManager.instance.Push(gameObject);
    }

    private IEnumerator LifeEffectCor()
    {
        yield return new WaitForSeconds(liveTime);

        DisableEffect();
    }
}
