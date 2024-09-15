using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Beta : PlayerBase
{
    [Header("BetaBase")]
    [SerializeField] protected float dashSpeed = 15f;
    [SerializeField] protected float dashDuration = 0.2f;

    private bool isDash;

    protected override void FixedUpdate()
    {
        if (isDash) return;

        base.FixedUpdate();
    }

    protected override void Skill()
    {
        if (!pSkill) return;

        isDash = true;

        AudioManager.Instance.StartSfx($"Dash");

        Vector2 dashVelocity = new Vector2(isFacingRight ? dashSpeed : -dashSpeed, rb.velocity.y);
        SetRigidbody(dashVelocity);

        StartCoroutine(DashDelay());

        int spinDown = isFacingRight ? -180 : 180;
        int spinUp = isFacingRight ? -360 : 360;

        isInvincibility = true;
        sp.color = Color.gray;

        DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 0, spinDown), dashDuration / 2f))
            .Append(transform.DORotate(new Vector3(0, 0, spinUp), dashDuration / 2f))
            .OnComplete(() => 
            {
                isInvincibility = false;
                sp.color = Color.white;
                transform.rotation = new Quaternion(0, 0, 0, 0);
            });

        base.Skill();
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

    private IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(dashDuration);

        isDash = false;
        SetRigidbody(Vector2.zero);
    }
}
