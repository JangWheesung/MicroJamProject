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

    // 초기화 함수
    public void Initialize(RankDataJSON data, int rank, RankType type)
    {
        this.data = data;

        // characterSpriteName을 이용해 Resources 폴더에서 스프라이트 로드
        rankImage.sprite = Resources.Load<CharacterStat>($"CharcterStats/{data.characterName}Stat").characterSprite;

        // 랭크 순위 텍스트 설정
        rankAmountText.text = $"{rank}.";

        // 닉네임 텍스트 설정
        nicknameText.text = data.nickName;

        // 랭크 타입에 따른 스코어 텍스트 설정
        scoreText.text = type switch
        {
            RankType.Time => $"최대 생존시간 : <size={scoreTextSize}><color={scoreColor}>{data.timeCount.ToString("F1")}s",
            RankType.Kill => $"최대 킬 수 : <size={scoreTextSize}><color={scoreColor}>{data.killCount}",
            _ => "Null",
        };

        // 1등일 때와 그 외의 경우에 따른 패널 이미지 설정
        rankPanel.sprite = rank == 1 ? firstPanelSprite : otherPanelSprite;
    }
}
