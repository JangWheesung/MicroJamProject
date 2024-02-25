using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleEffect : MonoBehaviour
{
    private SpriteRenderer sp;

    private void OnEnable()
    {
        sp = GetComponent<SpriteRenderer>();

        transform.DOScale(new Vector3(3, 3, 1), 0.5f).SetEase(Ease.OutElastic).OnComplete(() => 
        {
            sp.DOFade(0.2f, 0.2f).OnComplete(() =>
            {
                OrgSet();
                PoolingManager.instance.Push(gameObject);
            });
        });
    }

    private void OrgSet()
    {
        Color color = new Color(1, 1, 1, 1);
        sp.color = color;
        transform.localScale = Vector3.one;
    }
}
