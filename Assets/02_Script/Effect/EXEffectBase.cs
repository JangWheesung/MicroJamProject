using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EXEffectBase : EffectBase
{
    [SerializeField] protected EffectBase normalEffect;
    [SerializeField] protected float attackAmount;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void DisableEffect()
    {
        foreach (GameObject enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemyObj.TryGetComponent(out IEnemy enemy))
            {
                enemy.Death(attackAmount);
                PoolingManager.Instance.Pop<EffectBase>(normalEffect.name, enemyObj.transform.position).PopEffect();

                UISystem.Instance.GetKillCount();
            }
        }
        ControlSystem.Instance.SetEX(false);

        SpecialEffectSystem.Instance.BackgroundDarkness(0.1f, false);
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);

        base.DisableEffect();
    }
}
