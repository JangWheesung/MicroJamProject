using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickEffect :NormalEffectBase
{
    private Animator anim;

    public override void PopEffect()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Smashing", true);

        float rotateZ = Random.Range(-30f, 30f);
        transform.Rotate(new Vector3(0, 0, rotateZ));
    }

    protected override void DisableEffect()
    {
        anim.SetBool("Smashing", false);

        base.DisableEffect();
    }
}
