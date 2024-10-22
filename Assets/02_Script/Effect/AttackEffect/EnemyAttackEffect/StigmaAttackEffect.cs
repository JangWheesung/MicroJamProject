using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StigmaAttackEffect : SmashingAttackEffect
{
    public void PopEffect(Vector2 vec, IEnemy enemy)
    {
        base.PopEffect(enemy);
        base.PopEffect(vec);
    }
}
