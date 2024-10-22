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
    public virtual void PopEffect(Vector2 vec) { }
    public virtual void PopEffect(object value) { }
    public virtual void PopEffect(PlayerBase player) { }
    public virtual void PopEffect(IEnemy enemy) { }

    public virtual void UnityAnimEvent() { }

    public virtual void DisableEffect()
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
