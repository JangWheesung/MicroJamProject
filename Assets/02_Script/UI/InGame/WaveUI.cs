using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private RectTransform rootUI;
    [SerializeField] private TMP_Text waveCntText;
    [SerializeField] private TMP_Text waveReadingText;

    public void PopWaveSetting(int waveCnt, string reading)
    {
        waveCntText.text = $"Wave : <color=red>{waveCnt}";
        waveReadingText.text = reading;

        DOTween.Sequence()
            .Append(rootUI.DOLocalMoveX(0, 1f).SetEase(Ease.OutExpo))
            .Append(rootUI.DOLocalMoveX(-1500, 1f).SetEase(Ease.InExpo))
            .AppendCallback(() => { 
                rootUI.transform.localPosition = new Vector3(1500, rootUI.transform.localPosition.y, 0f); 
            });
    }
}
