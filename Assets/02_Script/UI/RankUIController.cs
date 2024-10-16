using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public enum RankType
{
    Time,
    Kill,
};

public class RankUIController : MonoBehaviour
{
    [Header("RankPanel")]
    [SerializeField] private RectTransform rankRoot;
    [SerializeField] private RectTransform content;
    [SerializeField] private RankSlot rankSlotObj;

    [Header("RankChange")]
    [SerializeField] private Button rankChangeBtn;
    [SerializeField] private Image rankChangeImage;
    [SerializeField] private Sprite timeSprite;
    [SerializeField] private Sprite killSprite;

    private RankSlot[] contentRankSlots = new RankSlot[99];
    private RankType rankType;

    private void Awake()
    {
        rankType = RankType.Time;
        rankChangeBtn.onClick.AddListener(() => { RankTypeChange(); });

        RankInitializer();
    }

    private void Start()
    {
        rankRoot.DOLocalMoveY(0f, 0.5f).SetEase(Ease.OutBack);
    }

    private void RankInitializer()
    {
        RankData[] rankDatas = Resources.LoadAll<RankData>("RankData");

        if (rankType == RankType.Time)
            rankDatas = rankDatas.OrderByDescending(data => data.timeCount).ToArray();
        else
            rankDatas = rankDatas.OrderByDescending(data => data.killCount).ToArray();

        int rankCount = 1;
        foreach (var data in rankDatas)
        {
            RankSlot slot;

            if (content.childCount < rankCount)
            {
                slot = Instantiate(rankSlotObj, content);
                slot.Initialize(data, rankCount, rankType);

                contentRankSlots[rankCount - 1] = slot;
            }
            else
            {
                contentRankSlots[rankCount - 1].Initialize(data, rankCount, rankType);
                contentRankSlots[rankCount - 1].transform.DOShakePosition(0.1f);
                contentRankSlots[rankCount - 1].transform.DOShakeScale(0.1f);
            }

            rankCount++;
        }
    }

    public void RankTypeChange()
    {
        if (rankType == RankType.Time)
        {
            rankType = RankType.Kill;
            rankChangeBtn.transform.DOLocalMoveX(0.7f, 0.1f);
            rankChangeImage.sprite = killSprite;
        }
        else
        {
            rankType = RankType.Time;
            rankChangeBtn.transform.DOLocalMoveX(-0.7f, 0.1f);
            rankChangeImage.sprite = timeSprite;
        }

        RankInitializer();
    }
}
