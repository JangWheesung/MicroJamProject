using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using System.IO;

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

    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.K))
        {
            Debug.Log("Rank");
            RankDestroyCommanderKey();
        }
    }

    private void RankDestroyCommanderKey()
    {
        string directoryPath = $"{Application.streamingAssetsPath}";
        string filePath = $"{directoryPath}/RankData.json";

        if (!Directory.Exists(directoryPath)) return;

        List<RankDataJSON> rankDataList = new List<RankDataJSON>();
        RankDataList rankDataWrapper = new RankDataList { rankDatas = rankDataList };
        string updatedJson = JsonUtility.ToJson(rankDataWrapper, true);

        File.WriteAllText(filePath, updatedJson);

        RankInitializer();
    }

    private void RankInitializer()
    {
        // JSON 파일 경로
        string path = $"{Application.streamingAssetsPath}/RankData.json";

        // JSON 파일에서 RankData 불러오기
        if (!File.Exists(path)) return;

        string jsonData = File.ReadAllText(path);
        List<RankDataJSON> rankDataList = JsonUtility.FromJson<RankDataList>(jsonData).rankDatas;

        // 랭크 타입에 따른 정렬
        if (rankType == RankType.Time)
            rankDataList = rankDataList.OrderByDescending(data => data.timeCount).ToList();
        else
            rankDataList = rankDataList.OrderByDescending(data => data.killCount).ToList();

        int rankCount = 1;
        foreach (var data in rankDataList)
        {
            RankSlot slot;

            // Resources 폴더에서 스프라이트 로드
            //Sprite characterSprite = Resources.Load<Sprite>($"CharacterSprites/{data.characterSpriteName}");

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

    public void OnChoiceCilck()
    {
        if (PlayerPrefs.GetString("PlayerName") == "") return;

        rankRoot.DOLocalMoveY(10f, 0.2f).SetEase(Ease.InQuart);
    }
}
