using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private float liveTime;

    protected Coroutine lifeCor;

    protected virtual void OnEnable()
    {
        lifeCor = StartCoroutine(LifeEffectCor());
    }

    public virtual void PopEffect() { }

    protected virtual void DisableEffect()
    {
        if (lifeCor != null)
            StopCoroutine(lifeCor);

        PoolingManager.Instance.Push(gameObject);
    }

    private IEnumerator LifeEffectCor()
    {
        yield return new WaitForSeconds(liveTime);

        DisableEffect();
    }
}
