using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HetaAttackEffect : AttackEffectBase
{
    [SerializeField] private Transform colBox;

    public override void PopEffect()
    {
        HitRader(transform.position, colBox.localScale, 0f);
    }

    protected override void PlayerHit(PlayerBase player)
    {
        player.Hit();

        TimeSystem.Instance.MinusTime(10);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, colBox.localScale);
    }
#endif
}
