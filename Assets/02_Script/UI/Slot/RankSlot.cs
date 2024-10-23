using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankSlot : MonoBehaviour
{
    public RankDataJSON data { get; private set; }

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

    // �ʱ�ȭ �Լ�
    public void Initialize(RankDataJSON data, int rank, RankType type)
    {
        this.data = data;

        // characterSpriteName�� �̿��� Resources �������� ��������Ʈ �ε�
        rankImage.sprite = Resources.Load<CharacterStat>($"CharcterStats/{data.characterName}Stat").characterSprite;

        // ��ũ ���� �ؽ�Ʈ ����
        rankAmountText.text = $"{rank}.";

        // �г��� �ؽ�Ʈ ����
        nicknameText.text = data.nickName;

        // ��ũ Ÿ�Կ� ���� ���ھ� �ؽ�Ʈ ����
        scoreText.text = type switch
        {
            RankType.Time => $"�ִ� �����ð� : <size={scoreTextSize}><color={scoreColor}>{data.timeCount.ToString("F1")}s",
            RankType.Kill => $"�ִ� ų �� : <size={scoreTextSize}><color={scoreColor}>{data.killCount}",
            _ => "Null",
        };

        // 1���� ���� �� ���� ��쿡 ���� �г� �̹��� ����
        rankPanel.sprite = rank == 1 ? firstPanelSprite : otherPanelSprite;
    }
}
