using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Delta : PlayerBase
{
    [Header("DeltaBase")]
    [SerializeField] private EffectBase skillEffect;
    [SerializeField] private float skillTime;
    [SerializeField] private LayerMask enemyLayer;

    protected override void Skill()
    {
        if (!pSkill) return;

        StartCoroutine(SkillCoolCor());

        base.Skill();
    }

    protected override void Attack()
    {
        if (!pAttack) return;

        pAttack = false;
        StartCoroutine(AttackDelay());

        Vector2 effectPos = transform.position + (MouseVec() * 0.8f);

        AttackEffectBase effect = PoolingManager.Instance.Pop<AttackEffectBase>(attackEffect.name, effectPos);
        effect.SetTimeAmount(attackValue);
        effect.PopEffect(-MouseVec());

        //AudioManager.Instance.StartSfx();
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Rock_H, 0.2f);
    }

    protected override void EX()
    {
        rb.velocity = Vector2.zero;

        EXEffectBase effect = PoolingManager.Instance.Pop<EXEffectBase>(exEffect.name, Vector2.zero);
        effect.SetTimeAmount(exValue);
        effect.PopEffect(this);
    }

    private IEnumerator SkillCoolCor()
    {
        isInvincibility = true;
        jumpCount--;
        moveSpeed /= 2;

        yield return new WaitForSeconds(skillTime);

        isInvincibility = false;
        jumpCount++;
        moveSpeed *= 2;
    }
}
