using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Beta : PlayerBase
{
    protected override void Dash()
    {
        if (!pDash) return;

        base.Dash();

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
    }

    protected override void Attack()
    {
        if (!pAttack) return;

        pAttack = false;
        StartCoroutine(AttackDelay());

        Vector2 effectPos = transform.position + (MouseVec() * 3.5f);

        AttackEffeectBase effect = PoolingManager.Instance.Pop<AttackEffeectBase>(attackEffect.name, effectPos);
        effect.PopEffect(-MouseVec());

        AudioManager.Instance.StartSfx($"Smashing3");
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);
    }

    protected override void EX()
    {
        rb.velocity = Vector2.zero;

        EXEffectBase effect = PoolingManager.Instance.Pop<EXEffectBase>(exEffect.name, Vector2.zero);
        effect.PopEffect(this);
    }
}
