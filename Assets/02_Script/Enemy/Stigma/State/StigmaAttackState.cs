using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StigmaAttackState : StigmaBaseState
{
    public StigmaAttackState(StigmaFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateEnter()
    {
        rb.gravityScale = 0f;

        float distance = Mathf.Clamp(playerTrs.position.x - stigmaFSM.transform.position.x, -1f, 1f);
        sp.flipX = distance <= 0 ? true : false;

        anim.SetBool("Fade", true);
        StartCoroutine(AttackCor());
    }

    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(0.4f);

        rb.gravityScale = stigmaFSM.gravityScale;

        StigmaAttackEffect effect = PoolingManager.Instance.Pop<StigmaAttackEffect>(smashinEffect.name, stigmaFSM.transform.position);
        effect.SetTimeAmount(attackAmount);
        effect.PopEffect(-LookPlayerVec(), stigmaFSM);

        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_M);
        AudioManager.Instance.StartSfx($"Smashing_4");
    }
}
