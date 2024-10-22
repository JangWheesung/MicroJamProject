using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasitAttackEffect : CircleAttackEffect
{
    public override void PopEffect(IEnemy enemy)
    {
        base.PopEffect(enemy);
        base.PopEffect();
    }

    public override void DisableEffect()
    {
        Color color = new Color(1, 0.5f, 1, 1);
        sp.color = color;
        transform.localScale = Vector3.one;

        base.DisableEffect();
    }
}
