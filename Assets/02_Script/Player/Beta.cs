using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Beta : PlayerBase
{
    [Header("BetaBase")]
    [SerializeField] protected float parryingDuration = 0.2f;

    private bool isParrying;

    protected override void Skill()
    {
        if (!pSkill) return;

        isParrying = true;
        SetSpriteColor(Color.gray);
        SetRigidbody(Vector2.zero);

        StartCoroutine(ParryingDelay());

        base.Skill();
    }

    private void Parrying(IEnemy enemy)
    {
        isParrying = false;

        StopCoroutine(ParryingDelay());

        transform.DOLocalMove(enemy.EnemyPos(), 0.05f)
            .OnComplete(() => 
            {
                SetSpriteColor(Color.white);
                enemy.Death(attackAmount);
            });
    }

    public override void Hit(IEnemy enemy, float minusTime)
    {
        if (isParrying)
        {
            Parrying(enemy);
            return;
        }
        base.Hit(enemy, minusTime);
    }

    protected override void Attack()
    {
        if (!pAttack) return;

        pAttack = false;
        StartCoroutine(AttackDelay());

        Vector2 effectPos = transform.position + (MouseVec() * 3.5f);

        AttackEffectBase effect = PoolingManager.Instance.Pop<AttackEffectBase>(attackEffect.name, effectPos);
        effect.SetTimeAmount(attackValue);
        effect.PopEffect(-MouseVec());

        AudioManager.Instance.StartSfx($"Smashing_3");
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);
    }

    protected override void EX()
    {
        rb.velocity = Vector2.zero;

        EXEffectBase effect = PoolingManager.Instance.Pop<EXEffectBase>(exEffect.name, Vector2.zero);
        effect.SetTimeAmount(exValue);
        effect.PopEffect(this);
    }

    private IEnumerator ParryingDelay()
    {
        yield return new WaitForSeconds(parryingDuration);

        isParrying = false;
        SetSpriteColor(Color.white);
        SetRigidbody(Vector2.zero);
    }
}
