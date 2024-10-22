using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Alpha : PlayerBase
{
    [Header("AlphaBase")]
    [SerializeField] protected BoostEffect skillEffect_smash;
    [SerializeField] protected AttackEffectBase skillEffect_line;
    [SerializeField] protected EffectBase skillEffect_circle;
    [SerializeField] protected float dashSpeed;
    [SerializeField] protected float dashDuration;

    private Vector2 startDashPoint;
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
        isInvincibility = true;
        startDashPoint = transform.position;

        AudioManager.Instance.StartSfx($"Dash");

        Vector2 dashVelocity = new Vector2(isFacingRight ? dashSpeed : -dashSpeed, rb.velocity.y);
        SetRigidbody(dashVelocity);

        StartCoroutine(DashDelay());

        base.Skill();
    }

    private void BackDash()
    {
        isDash = false;

        StopCoroutine(DashDelay());

        Vector2 startBackDashPoint = transform.position;
        Vector2 endBakDashPoint = startDashPoint;
        float distance = Vector2.Distance(startBackDashPoint, endBakDashPoint);
        Vector2 direction = (endBakDashPoint - startBackDashPoint).normalized;

        PoolingManager.Instance.Pop<BoostEffect>(skillEffect_smash.name, transform.position).PopEffect(this, direction, false);
        PoolingManager.Instance.Pop<EffectBase>(skillEffect_circle.name, transform.position).PopEffect();

        SetRigidbody(Vector2.zero);
        SetFacingDirection(direction);

        for (int i = 0; i < distance; i++)
        {
            Vector2 point = startBackDashPoint + direction * i;

            var lineEffect = PoolingManager.Instance.Pop<AttackEffectBase>(skillEffect_line.name, point);
            lineEffect.SetTimeAmount(attackAmount);
            lineEffect.PopEffect();

            AudioManager.Instance.StartSfx($"Smashing_1");
            SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_S);
        }
        
        transform.DOLocalMove(startDashPoint, 0.1f);
        isInvincibility = false;
    }

    protected override void Attack()
    {
        if (!pAttack) return;
        if (isDash)
        {
            BackDash();
            return;
        }

        pAttack = false;
        StartCoroutine(AttackDelay());

        Vector2 effectPos = transform.position + (MouseVec() * 2f);

        AttackEffectBase effect = PoolingManager.Instance.Pop<AttackEffectBase>(attackEffect.name, effectPos);
        effect.SetTimeAmount(attackValue);
        effect.PopEffect(-MouseVec());

        int idx = Random.Range(1, 3);
        AudioManager.Instance.StartSfx($"Smashing_{idx}"); //1 ~ 2
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_M);
    }

    private IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(dashDuration);

        isDash = false;
        isInvincibility = false;
        SetRigidbody(Vector2.zero);
    }
}
