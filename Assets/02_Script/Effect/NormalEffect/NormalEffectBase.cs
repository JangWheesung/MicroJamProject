using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEffectBase : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private float liveTime;

    private Coroutine lifeCor;

    private void OnEnable()
    {
        lifeCor = StartCoroutine(LifeEffectCor());
    }

    public virtual void PopEffect() 
    {
        DisableEffect();
    }

    protected virtual void DisableEffect()
    {
        if (lifeCor != null)
            StopCoroutine(lifeCor);

        PoolingManager.instance.Push(gameObject);
    }

    private IEnumerator LifeEffectCor()
    {
        yield return new WaitForSeconds(liveTime);

        DisableEffect();
    }
}
