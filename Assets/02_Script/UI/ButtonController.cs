using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public UnityEvent OnClick;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(ButtonClickHandle);
    }

    private void ButtonClickHandle()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(Vector3.one * 0.9f, 0.2f)).SetEase(Ease.OutQuad);
        seq.Append(transform.DOScale(Vector3.one * 1, 0.2f)).SetEase(Ease.OutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            AudioManager.Instance.StartSfx("Click");
            OnClick?.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.OutQuad);

        AudioManager.Instance.StartSfx("ClickUp");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuad);
    }
}
