using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Beta : PlayerBase
{
    [Header("BetaBase")]
    [SerializeField] protected BoostEffect skillEffect_set;
    [SerializeField] protected EffectBase skillEffect_x;
    [SerializeField] protected AttackEffectBase skillEffect_attack;
    [SerializeField] protected float parryingDuration;
    [SerializeField] protected float parryingDelay;

    private BoostEffect skillSetEffect;
    private bool isParrying;

    protected override void Skill()
    {
        if (!pSkill) return;
        
        isParrying = true;
        SetSpriteColor(Color.gray);

        skillSetEffect = PoolingManager.Instance.Pop<BoostEffect>(skillEffect_set.name, transform.position);
        skillSetEffect.PopEffect(this);

        StartCoroutine(ParryingDelay());

        AudioManager.Instance.StartSfx($"Boost", 0.6f);

        base.Skill();
    }

    private void Parrying(IEnemy enemy)
    {
        isParrying = false;

        StopCoroutine(ParryingDelay());

        PoolingManager.Instance.Pop<EffectBase>(skillEffect_x.name, transform.position).PopEffect();

        SpecialEffectSystem.Instance.BackgroundDarkness(parryingDelay);
        AudioManager.Instance.StartSfx($"BetaSkill");

        DOTween.Sequence()
            .Append(DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0.2f, parryingDelay).SetUpdate(true))
            .AppendCallback(() => { Time.timeScale = 1f; })
            .Append(transform.DOLocalMove(enemy.EnemyPos(), 0.1f))
            .AppendCallback(() => 
            {
                SetSpriteColor(Color.white);
                enemy.Death(attackAmount);

                var skillAttackEffect = PoolingManager.Instance.Pop<AttackEffectBase>(skillEffect_attack.name, transform.position);
                skillAttackEffect.SetTimeAmount(attackAmount);
                skillAttackEffect.PopEffect();

                skillSetEffect.DisableEffect();

                SpecialEffectSystem.Instance.BackgroundDarkness(0.1f, false);
                AudioManager.Instance.StartSfx($"Smashing_3");
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
