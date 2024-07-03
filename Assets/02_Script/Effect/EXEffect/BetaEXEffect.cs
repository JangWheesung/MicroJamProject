using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaEXEffect : EXEffectBase
{
    public override void PopEffect(PlayerBase player)
    {

    }

    public override void UnityAnimEvent() 
    {
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);
    }
}
