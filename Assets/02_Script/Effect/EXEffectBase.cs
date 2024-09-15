using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EXEffectBase : EffectBase
{
    [SerializeField] protected EffectBase normalEffect;

    protected float timeAmount;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public void SetTimeAmount(float timeAmount)
    {
        this.timeAmount = timeAmount;
    }

    public override void DisableEffect()
    {
        foreach (GameObject enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemyObj.TryGetComponent(out IEnemy enemy))
            {
                enemy.Death(timeAmount);
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
