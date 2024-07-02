using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXEffectBase : EffectBase
{
    protected override void OnEnable()
    {


        base.OnEnable();
    }

    public override void PopEffect() { }
    public virtual void PopEffect(Vector2 vec) { }
    public virtual void PopEffect(object value) { }

    protected override void DisableEffect()
    {
        //��� ���� ���̱�
        SpecialEffectSystem.Instance.BackgroundDarkness(0.1f, false);

        base.DisableEffect();
    }
}
