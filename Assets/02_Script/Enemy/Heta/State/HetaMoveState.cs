using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaMoveState : HetaBaseState
{
    public HetaMoveState(HetaFSM fsm) : base(fsm) { }

    protected override void OnStateUpdate()
    {
        float distance = Mathf.Clamp(playerTrs.position.x - hetaFSM.transform.position.x, -1f, 1f);
        rb.velocity = new Vector2(distance * moveSpeed, rb.velocity.y);
        sp.flipX = distance <= 0 ? true : false;

        if (Mathf.Abs(rb.velocity.y) == 0)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}