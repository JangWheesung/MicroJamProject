using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AlphaEXEffect : EXEffectBase
{
    public override void UnityAnimEvent()
    {
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);
        AudioManager.Instance.StartSfx("Smashing_4");
    }
}
