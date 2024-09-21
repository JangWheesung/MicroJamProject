using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaAttackEffect : BulletAttackEffect
{
    public void PopEffect(Vector2 vec, bool isUpgrade)
    {
        bulletPenetrate = isUpgrade;

        PopEffect(vec);
    }
}
