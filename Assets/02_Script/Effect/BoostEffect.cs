using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : EffectBase
{
    public override void PopEffect(PlayerBase player, bool isParent = false)
    {
        transform.position = player.transform.position;
        transform.parent = player.transform;
    }

    public void PopEffect(PlayerBase player, Vector2 vec, bool isUp = true)
    {
        if (isUp)
            transform.up = vec;
        else
            transform.right = vec;

        PopEffect(player);
    }
}
