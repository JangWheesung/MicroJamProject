using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UX : MonoBehaviour
{
    [SerializeField] private Image[] panels;
    [SerializeField] private GameObject pressObj;

    private void Start()
    {
        ControlSystem.Instance.OnGameStartEvt += CloseUXUI;
    }

    public void PopUXUI()
    {
        foreach (Image image in panels)
        {
            image.DOFade(0.9f, 1f);
        }
    }

    public void CloseUXUI()
    {
        pressObj.SetActive(false);

        foreach (Image image in panels)
        {
            DOTween.Sequence()
                .Append(image.DOFade(0f, 1f))
                .AppendCallback(() => { gameObject.SetActive(false); });
        }
    }
}
