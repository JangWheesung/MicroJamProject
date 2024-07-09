using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EXEffectBase : EffectBase
{
    [SerializeField] protected EffectBase normalEffect;

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
                enemy.Death();
                PoolingManager.Instance.Pop<EffectBase>(normalEffect.name, enemyObj.transform.position).PopEffect();

                UISystem.Instance.GetKillCount();
                TimeSystem.Instance.PlusTime(1f);
            }
        }
        ControlSystem.Instance.SetEX(false);

        SpecialEffectSystem.Instance.BackgroundDarkness(0.1f, false);
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);

        base.DisableEffect();
    }
}
