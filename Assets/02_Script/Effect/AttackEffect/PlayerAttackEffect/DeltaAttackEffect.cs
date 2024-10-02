using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaAttackEffect : BoxAttackEffect
{
    [SerializeField] private float shakeRange;

    public override void PopEffect(Vector2 vec) 
    {
        float randomShake = Random.Range(-shakeRange / 2, shakeRange / 2);

        float nowAngle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        float newAngle = nowAngle + randomShake;

        float radian = newAngle * Mathf.Deg2Rad;
        Vector2 shakenVec = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

        base.PopEffect(shakenVec);
    }
}
