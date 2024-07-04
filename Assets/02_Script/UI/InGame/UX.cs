using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UX : MonoBehaviour
{
    [SerializeField] private Image[] panels;

    public void PopUXUI()
    {
        foreach (Image image in panels)
        {
            DOTween.Sequence()
                .Append(image.DOFade(0.9f, 1f))
                .AppendInterval(1f)
                .Append(image.DOFade(0f, 1f))
                .AppendCallback(() => { gameObject.SetActive(false); });
        }
    }
}
