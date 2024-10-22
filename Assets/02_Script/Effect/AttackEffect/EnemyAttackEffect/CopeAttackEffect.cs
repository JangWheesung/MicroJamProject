using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopeAttackEffect : SmashingAttackEffect
{
    public override void PopEffect(IEnemy enemy)
    {
        base.PopEffect(enemy);

        transform.localScale = new Vector3(attackRadius * 5f, attackRadius * 5f);
        base.PopEffect();
    }
}
