using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAttackEffect : AttackEffectBase
{
    [SerializeField] private Transform colBox;

    public override void PopEffect()
    {
        HitRader(transform.position, colBox.localScale, 0f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, colBox.localScale);
    }
#endif
}
