using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaAimingState : HetaBaseState
{
    private EffectBase effectObj;

    public HetaAimingState(HetaFSM fsm) : base(fsm) { }

    protected override void OnStateEnter()
    {
        effectObj = PoolingManager.Instance.Pop<EffectBase>(crosshairEffect.name, playerTrs.position);
        effectObj.PopEffect(player);

        StartCoroutine(LineCor());
    }

    protected override void OnStateUpdate()
    {
        float distance = Mathf.Clamp(playerTrs.position.x - hetaFSM.transform.position.x, -1f, 1f);
        sp.flipX = distance <= 0 ? true : false;

        hetaFSM.aimingLine.SetPosition(0, hetaFSM.transform.position);
        hetaFSM.aimingLine.SetPosition(1, playerTrs.position);
    }

    protected override void OnStateExit()
    {
        
    }

    private IEnumerator LineCor()
    {
        float currentTime = 0.3f;
        float initialTime = 0.7f;
        float delayTime = 0.05f;

        float time = initialTime;
        float twoThirdsDelay = aimingDelay * 2 / 3;

        while (currentTime < aimingDelay)
        {
            hetaFSM.aimingLine.enabled = true;
            yield return new WaitForSeconds(time);

            hetaFSM.aimingLine.enabled = false;
            yield return new WaitForSeconds(delayTime);

            currentTime += (time + delayTime);

            if (currentTime < twoThirdsDelay)
                time = Mathf.Max(time - 0.2f, delayTime);
            else
                time = delayTime;

            AudioManager.Instance.StartSfx("Alarm", 0.3f);
        }

        effectObj.DisableEffect();
    }
}
