using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasitMoveState : HasitBaseState
{
    private float currentTime;

    public HasitMoveState(HasitFSM fsm) : base(fsm) { }

    protected override void OnStateUpdate()
    {
        if (currentTime <= 0)
        {
            float distance = Mathf.Clamp(playerTrs.position.x - hasitFSM.transform.position.x, -1f, 1f);
            rb.velocity = new Vector2(distance * moveSpeed, rb.velocity.y);
            sp.flipX = distance <= 0 ? true : false;

            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            currentTime = moveDelay;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    protected override void OnStateExit()
    {
        currentTime = 0;
    }
}
