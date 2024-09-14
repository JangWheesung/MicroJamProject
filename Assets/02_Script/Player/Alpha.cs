using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha : PlayerBase
{
    [Header("AlphaBase")]
    [SerializeField] protected float dashSpeed;
    [SerializeField] protected float dashDuration;

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

        base.Skill();
    }

    protected override void Attack()
    {
        if (!pAttack) return;

        pAttack = false;
        StartCoroutine(AttackDelay());

        Vector2 effectPos = transform.position + (MouseVec() * 2f);

        AttackEffectBase effect = PoolingManager.Instance.Pop<AttackEffectBase>(attackEffect.name, effectPos);
        effect.PopEffect(-MouseVec());

        int idx = Random.Range(1, 3);
        AudioManager.Instance.StartSfx($"Smashing_{idx}"); //1 ~ 2
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_M);
    }

    private IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(dashDuration);

        isDash = false;
        SetRigidbody(Vector2.zero);
    }
}
