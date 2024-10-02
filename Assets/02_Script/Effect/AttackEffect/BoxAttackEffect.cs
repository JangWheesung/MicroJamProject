using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAttackEffect : AttackEffectBase
{
    [SerializeField] private Transform colBox;

    public override void PopEffect()
    {
        HitRader(colBox.transform.position, colBox.localScale, 0f);
    }

    public override void PopEffect(Vector2 vec)
    {
        transform.up = vec;
        
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg - 90f;

        HitRader(colBox.transform.position, colBox.localScale, angle);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, colBox.localScale);
    }
#endif
}
