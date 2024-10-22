using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WahidMeleeAttackEffect : CircleAttackEffect
{
    public override void PopEffect(IEnemy enemy)
    {
        base.PopEffect(enemy);
        base.PopEffect();
    }

    public override void DisableEffect()
    {
        Color color = new Color(0, 0.8f, 1, 1);
        sp.color = color;
        transform.localScale = Vector3.one;

        base.DisableEffect();
    }
}
