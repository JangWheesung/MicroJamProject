using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

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
            ? "시작 화면으로" : "랭킹 등록하기";
    }

    public void GameoverPanel(float time, float kill)
    {
        AudioManager.Instance.StartSfx("GameEnd");

        score_time = time;
        score_kill = kill;

        highSocre_timeText.text = $"Time : {score_time}";
        highSocre_killText.text = $"Kill : {score_kill}";

        bool highTime = score_time > Resources.LoadAll<RankData>("RankData").Max(x => x.timeCount);
        bool highKill = score_kill > Resources.LoadAll<RankData>("RankData").Max(x => x.killCount);
        if (highTime || highKill)
        {
            newScoreText.text = "신기록 달성!";
        }

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
        Sprite playerSprite = Resources.Load<CharacterStat>($"CharcterStats/{playerName}Stat").characterSprite;

        int rankCount = Resources.LoadAll<RankData>("RankData").Length;
        string path = $"Assets/06_SO/Resources/RankData/RankData_{rankCount + 1}.asset"; //{playerName}RankData_{rankCount + 1}

        RankData rankData = ScriptableObject.CreateInstance<RankData>();
        rankData.Initialize(playerSprite, playerName, rankField.text, score_kill, score_time);

        AssetDatabase.CreateAsset(rankData, path);
        AssetDatabase.SaveAssets();
    }
}
