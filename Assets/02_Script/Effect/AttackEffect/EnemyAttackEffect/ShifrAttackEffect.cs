using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShifrAttackEffect : CircleAttackEffect
{
    public override void PopEffect(IEnemy enemy)
    {
        base.PopEffect(enemy);
        base.PopEffect();
    }
}
