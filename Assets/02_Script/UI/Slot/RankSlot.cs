using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankSlot : MonoBehaviour
{
    public RankData data { get; private set; }

    [Header("RankPanel")]
    [SerializeField] private Image rankPanel;
    [SerializeField] private Sprite firstPanelSprite;
    [SerializeField] private Sprite otherPanelSprite;

    [Header("RankUI")]
    [SerializeField] private Image rankImage;
    [SerializeField] private TMP_Text rankAmountText;
    [SerializeField] private TMP_Text nicknameText;
    [SerializeField] private TMP_Text scoreText;

    readonly string scoreTextSize = "0.3";
    readonly string scoreColor = "red";

    public void Initialize(RankData data, int rank, RankType type)
    {
        this.data = data;
        rankImage.sprite = data.characterSprite;
        rankAmountText.text = $"{rank}.";
        nicknameText.text = data.nickName;
        scoreText.text = type switch
        {
            RankType.Time => $"최대 생존시간 : <size={scoreTextSize}><color={scoreColor}>{data.timeCount.ToString("F1")}s",
            RankType.Kill => $"최대 킬 수 : <size={scoreTextSize}><color={scoreColor}>{data.killCount}",
            _ => "Null",
        };

        if (rank == 1)
            rankPanel.sprite = firstPanelSprite;
        else
            rankPanel.sprite = otherPanelSprite;
    }
}
