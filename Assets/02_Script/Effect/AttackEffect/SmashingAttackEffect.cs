using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashingAttackEffect : AttackEffectBase
{
    [SerializeField] protected float attackRadius;

    public override void PopEffect()
    {
        HitRader(transform.position, attackRadius);
    }

    public override void PopEffect(Vector2 vec)
    {
        transform.up = vec;

        HitRader(transform.position, attackRadius);
    }

    public override void PopEffect(PlayerBase player, bool isParent = false)
    {
        HitRader(player.transform.position, attackRadius);

        if (isParent)
        {
            transform.position = player.transform.position;
            transform.parent = player.transform;
        }
    }
}
