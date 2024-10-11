using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : EffectBase
{
    public override void PopEffect(PlayerBase player)
    {
        transform.position = player.transform.position;
        transform.parent = player.transform;
    }
}
