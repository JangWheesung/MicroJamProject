using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickEffect :EffectBase
{
    public override void PopEffect()
    {
        float rotateZ = Random.Range(-30f, 30f);
        transform.Rotate(new Vector3(0, 0, rotateZ));
    }
}
