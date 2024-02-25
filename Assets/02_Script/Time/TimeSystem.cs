using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    public static TimeSystem Instance;

    public event Action OnGameoverEvt;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private float timeSettingValue;

    private Sequence effectSequence;

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
            float outTime = Mathf.Floor(nowTime * 10f) / 10f;
            timeText.text = $"{outTime}s";
        }
    }

    private void Awake()
    {
        Instance = this;
        NowTime = timeSettingValue;
    }

    private void Update()
    {
        NowTime -= Time.deltaTime;
    }

    public void PlusTime(float value)
    {
        if (NowTime + value > 120f)
            NowTime = 120f;
        else
            NowTime += value;

        TimeEffect(Color.blue);
    }

    public void MinusTime(float value)
    {
        if (NowTime - value <= 0f)
        {
            NowTime = 0f;
            OnGameoverEvt?.Invoke();
        }
        else
            NowTime -= value;

        TimeEffect(Color.red);
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
}
