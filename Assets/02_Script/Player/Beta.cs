using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Beta : PlayerBase
{
    protected override void Dash()
    {
        if (!possibleDashing) return;

        base.Dash();

        int spinDown = IsFacingRight ? -180 : 180;
        int spinUp = IsFacingRight ? -360 : 360;

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
        if (!isAttacking)
        {
            AudioManager.Instance.StartSfx($"Smashing3");

            isAttacking = true;
            StartCoroutine(AttackDelay());

            Vector2 effectPos = transform.position + (MouseVec() * 3.5f);

            AttackEffeectBase effect = PoolingManager.instance.Pop<AttackEffeectBase>(attackEffect.name, effectPos);
            effect.transform.up = -MouseVec();
            effect.PopEffect();

            CinemachineEffectSystem.Instance.CinemachineShaking();
        }
    }
}
