using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaAttackEffect : BoxAttackEffect
{
    public override void PopEffect(IEnemy enemy)
    {
        base.PopEffect(enemy);
        base.PopEffect();
    }
}
