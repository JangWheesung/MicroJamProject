using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXEffectBase : EffectBase
{
    [SerializeField] protected EffectBase normalEffect;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void PopEffect() { }
    public virtual void PopEffect(Vector2 vec) { }
    public virtual void PopEffect(object value) { }
    public virtual void PopEffect(PlayerBase player) { }

    public virtual void UnityAnimEvent() { }

    protected override void DisableEffect()
    {
        foreach (EnemyBase enemy in FindObjectsOfType<EnemyBase>())
        {
            enemy.Death();
            PoolingManager.Instance.Pop<EffectBase>(normalEffect.name, enemy.transform.position).PopEffect();

            UISystem.Instance.GetKillCount();
            TimeSystem.Instance.PlusTime(1f);
        }
        ControlSystem.Instance.SetEX(false);

        SpecialEffectSystem.Instance.BackgroundDarkness(0.1f, false);
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);

        base.DisableEffect();
    }
}
