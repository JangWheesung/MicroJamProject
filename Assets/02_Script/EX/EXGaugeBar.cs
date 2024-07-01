using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EXGaugeBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Image iconImage;
    private Slider slider;

    public bool IsCharging { get; private set; }

    private float currentGauge = 0f;
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

                SettingCharging();
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

        transform.DOLocalMoveY(480f, 0.5f).SetEase(Ease.OutBack);
    }

    private void SettingCharging()
    {
        fillImage.color = Color.white;

        SpecialEffectSystem.Instance.BackgroundAura(Color.cyan);
        SpecialEffectSystem.Instance.BloomIntensity(BloomType.Light_H);
    }

    public void PlusGauge(float value)
    {
        if (IsCharging) return;

        CurrentGauge += value;

        transform.DOShakeScale(0.2f).SetEase(Ease.OutBounce);
    }

    public void ChargingClear()
    {
        fillImage.color = Color.cyan;
        DOTween.To(() => CurrentGauge, x => CurrentGauge = x, 0f, 0.2f).SetEase(Ease.OutBack);
    }

    public Vector2 GetIconPos()
    {
        return iconImage.transform.position;
    }
}
