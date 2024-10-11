using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    public static TimeSystem Instance;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text highSocreText;
    [SerializeField] private float timeSettingValue;

    private Sequence effectSequence;

    public float TimeProduct { get; private set; }
    public float MaxTime { get; private set; }

    private float nowTime;
    public float NowTime 
    { 
        get 
        { 
            return nowTime;
        } 
        private set 
        {
            nowTime = value;

            if (nowTime <= 0f)
            {
                nowTime = 0f;
            }
            else if (nowTime >= MaxTime)
            {
                nowTime = MaxTime;
            }

            string outTime = nowTime.ToString("F1");
            timeText.text = $"{outTime}s";
        }
    }

    private void Awake()
    {
        Instance = this;
        MaxTime = timeSettingValue;
        NowTime = MaxTime;
        TimeProduct = 1f;
    }

    private void Update()
    {
        if (!ControlSystem.Instance.IsStopLogic())
        {
            NowTime -= Time.deltaTime * TimeProduct;
            if (NowTime <= 0f)
            {
                ControlSystem.Instance.SetDeath();
            }

            string highScore_str = UISystem.Instance.highSocre_time.ToString("F1") + "s";
            highSocreText.text = highScore_str;
        }
    }

    public void PlusTime(float value)
    {
        if (value == 0) return;

        AudioManager.Instance.StartSfx("TimeValue");

        NowTime += value;

        TimeEffect(Color.blue);
    }

    public void MinusTime(float value)
    {
        if (value == 0) return;

        AudioManager.Instance.StartSfx("TimeValue");

        NowTime -= value;

        TimeEffect(Color.red);
    }

    public void CustemTime(float value, Color color)
    {
        if (value == 0) return;

        AudioManager.Instance.StartSfx("TimeValue");

        NowTime = value;

        TimeEffect(color);
    }

    private void TimeEffect(Color effectColor)
    {
        timeText.color = effectColor;

        effectSequence.Kill();
        effectSequence = DOTween.Sequence();

        effectSequence
            .Prepend(timeText.transform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.1f))
            .Append(timeText.DOColor(Color.white, 0.3f))
            .Join(timeText.transform.DOScale(Vector3.one, 0.3f));
    }

    public void SettingMaxTime(float value)
    {
        MaxTime = value;
    }

    public void SettingTimeProduct(float value)
    {
        TimeProduct = value;
    }
}
