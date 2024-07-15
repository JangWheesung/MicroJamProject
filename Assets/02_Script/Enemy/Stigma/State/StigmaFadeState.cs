using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StigmaFadeState : StigmaBaseState
{
    public StigmaFadeState(StigmaFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateEnter()
    {
        stigmaFSM.isInvincibility = true;

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        trail.enabled = false;

        anim.SetBool("Fade", false);

        AudioManager.Instance.StartSfx($"FadeIn");
    }

    protected override void OnStateExit()
    {
        stigmaFSM.isInvincibility = false;

        rb.gravityScale = stigmaFSM.gravityScale;
        trail.enabled = true;

        Vector2 fadePos = playerTrs.position + (RandomPlayerAroundVec() * 1f);
        stigmaFSM.transform.position = fadePos;

        anim.SetBool("Fade", true);

        AudioManager.Instance.StartSfx($"FadeOut");
    }

    private Vector3 RandomPlayerAroundVec()
    {
        Vector3 randomVec = Random.insideUnitCircle.normalized;
        while (randomVec.y < 0)
        {
            randomVec = Random.insideUnitCircle.normalized;
        }
        return randomVec;
    }
}
