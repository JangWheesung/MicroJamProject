using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EXGaugeBar : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    private Slider slider;

    public bool IsCharging { get; private set; }
    private float currentGauge;
    public float CurrentGauge
    {
        get
        {
            return currentGauge;
        }
        set
        {
            if (value >= 1f)
            {
                IsCharging = true;
                currentGauge = 1f;
                iconImage.color = Color.cyan;
            }
            else
            {
                IsCharging = false;
                currentGauge = value;
                iconImage.color = Color.white;
            }
            slider.value = currentGauge;
        }
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void PlusGauge(float value)
    {
        if (IsCharging) return;

        CurrentGauge += value;

        iconImage.transform.DOShakeScale(0.2f).SetEase(Ease.OutBounce);
    }

    public void OutEnergePower()
    {
        currentGauge = 0f;
    }

    public Vector2 GetIconPos()
    {
        return iconImage.transform.position;
    }
}
