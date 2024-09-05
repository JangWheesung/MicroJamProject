using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CopeMoveState : CopeBaseState
{
    private Tween spinTween;

    public CopeMoveState(CopeFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateEnter()
    {
        Vector3 spinVec = LookPlayerVec().x > 0 ? new Vector3(0f, 0f, -360f) : new Vector3(0f, 0f, 360f);
        spinTween = copeFSM.transform.DORotate(spinVec, 0.7f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    protected override void OnStateUpdate()
    {
        float distance = Mathf.Clamp(playerTrs.position.x - copeFSM.transform.position.x, -1f, 1f);
        rb.velocity = new Vector2(distance * moveSpeed, rb.velocity.y);
    }

    protected override void OnStateExit()
    {
        rb.velocity = Vector2.zero;

        Vector3 spinVec = LookPlayerVec().x > 0 ? new Vector3(0f, 0f, -360f) : Vector2.zero;
        copeFSM.transform.DORotate(spinVec, 0.1f).OnComplete(() =>
        {
            spinTween.Kill();
            copeFSM.transform.rotation = Quaternion.identity;
        });
    }
}
