using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;

public class GameoverUI : MonoBehaviour
{
    [SerializeField] private RectTransform gameoverPanel;
    [SerializeField] private TextMeshProUGUI highSocre_timeText;
    [SerializeField] private TextMeshProUGUI highSocre_killText;
    [SerializeField] private TMP_Text newScoreText;
    [SerializeField] private TMP_InputField rankField;
    [SerializeField] private Button mainBtn;
    [SerializeField] private TMP_Text mainText;
    [SerializeField] private Image fadeImage;

    private float score_time;
    private float score_kill;

    private void Start()
    {
        mainBtn.onClick.AddListener(FadeInScene);
    }

    private void Update()
    {
        mainText.text = string.IsNullOrEmpty(rankField.text)
            ? "���� ȭ������" : "��ŷ ����ϱ�";
    }

    public void GameoverPanel(float time, float kill)
    {
        AudioManager.Instance.StartSfx("GameEnd");

        score_time = time;
        score_kill = kill;

        highSocre_timeText.text = $"Time : {score_time}";
        highSocre_killText.text = $"Kill : {score_kill}";

        //bool highTime = score_time > Resources.LoadAll<RankDataJSON>("RankData").Max(x => x.timeCount);
        //bool highKill = score_kill > Resources.LoadAll<RankDataJSON>("RankData").Max(x => x.killCount);
        //if (highTime || highKill)
        //{
        //    newScoreText.text = "�ű�� �޼�!";
        //}

        gameoverPanel.DOMoveY(0f, 0.5f).SetEase(Ease.OutBounce);
    }

    private void FadeInScene()
    {
        if (!string.IsNullOrEmpty(rankField.text))
        {
            SaveRankData();
        }

        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene("Intro");
        });
    }

    private void SaveRankData()
    {
        string playerName = PlayerPrefs.GetString("PlayerName");

        // ĳ���� ��������Ʈ �ε�
        Sprite playerSprite = Resources.Load<CharacterStat>($"CharcterStats/{playerName}Stat").characterSprite;

        // RankData�� ����� ���
        string directoryPath = $"{Application.streamingAssetsPath}";
        string filePath = $"{directoryPath}/RankData.json";

        // ���丮�� �������� ������ ����
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // ���� RankData�� JSON���� �ҷ��ͼ� ����Ʈ�� ��ȯ
        List<RankDataJSON> rankDataList = new List<RankDataJSON>();
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            rankDataList = JsonUtility.FromJson<RankDataList>(jsonData).rankDatas;
        }

        // ���ο� RankData ����
        RankDataJSON newRankData = new RankDataJSON(playerSprite, playerName, rankField.text, score_kill, score_time);
        rankDataList.Add(newRankData);

        // ����Ʈ�� �ٽ� JSON���� ����
        RankDataList rankDataWrapper = new RankDataList { rankDatas = rankDataList };
        string updatedJson = JsonUtility.ToJson(rankDataWrapper, true);

        // ���� ����
        File.WriteAllText(filePath, updatedJson);
    }
}
