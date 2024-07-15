using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WahidMoveState : WahidBaseState
{
    private float currentTime;

    public WahidMoveState(WahidFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateUpdate()
    {
        float distance = Mathf.Clamp(playerTrs.position.x - wahidFSM.transform.position.x, -1f, 1f);
        rb.velocity = new Vector2(distance * moveSpeed, rb.velocity.y); 
        sp.flipX = distance <= 0 ? true : false;

        float cos = Mathf.Cos(currentTime * waveInterval) * waveAmount;
        wahidFSM.transform.position += new Vector3(0, cos);

        float angle = (distance <= 0 ? -cos : cos) / waveAmount;
        wahidFSM.transform.right = new Vector3(1, angle).normalized;

        currentTime += Time.deltaTime;
    }

    protected override void OnStateExit()
    {
        rb.velocity = Vector2.zero;
    }
}
